using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;

namespace JAEE.AX.EditorExtensions
{
    /// <summary>
    /// Allow to minimice blocks of code in the editor
    /// </summary>
    internal sealed class OutliningTagger : ITagger<IOutliningRegionTag>
    {
        string[] startTokens = new string[] { "{", "/*"}; //, @"//region" };
        string[] endTokens = new string[] { "}", "*/" }; //, @"//endregion" };

        ITextBuffer buffer;
        ITextSnapshot snapshot;
        List<Region> regions;
        
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
        int maxRowsInToolTip;

        public OutliningTagger(ITextBuffer buffer)
        {
            this.buffer = buffer;
            this.snapshot = buffer.CurrentSnapshot;
            this.regions = new List<Region>();
            this.ReParse();
            this.buffer.Changed += BufferChanged;

            this.LoadSettings();
        }

        private void LoadSettings()
        {
            EditorSettings settings = EditorSettings.getInstance();

            maxRowsInToolTip = settings.Outlining.MaxRowsInTooltip;
        }


        public IEnumerable<ITagSpan<IOutliningRegionTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;

            List<Region> currentRegions = this.regions;
            ITextSnapshot currentSnapshot = this.snapshot;
            SnapshotSpan entire = new SnapshotSpan(spans[0].Start, spans[spans.Count - 1].End).TranslateTo(currentSnapshot, SpanTrackingMode.EdgeExclusive);
            int startLineNumber = entire.Start.GetContainingLine().LineNumber;
            int endLineNumber = entire.End.GetContainingLine().LineNumber;

            foreach (var region in currentRegions)
            {
                var collapsedText = string.Empty;
                var collapsedHint = string.Empty;
                var lastHintLine = string.Empty;

                if (region.StartLine <= endLineNumber && region.EndLine >= startLineNumber)
                {
                    var startLine = currentSnapshot.GetLineFromLineNumber(region.StartLine);
                    var endLine = currentSnapshot.GetLineFromLineNumber(region.EndLine);

                    int iLineCount = region.StartLine;
                    int iTotal = 0;

                    while (iLineCount <= region.EndLine)
                    {
                        string lineoftext = currentSnapshot.GetLineFromLineNumber(iLineCount).GetText();

                        if (iLineCount == region.StartLine + 1)
                        {
                            int spaces = lineoftext.TakeWhile(Char.IsWhiteSpace).Count();
                            lastHintLine = string.Empty;
                            for (int i = 0; i < spaces; i++) 
                                lastHintLine += ' ';
                            lastHintLine += "...\n";
                        }

                        if (iLineCount == region.EndLine)
                        {
                            collapsedHint += string.Format("{0}", lineoftext);
                        }
                        else
                        {
                            collapsedHint += string.Format("{0}\n", lineoftext);
                        }

                        iLineCount++;
                        iTotal++;
                        
                        if (maxRowsInToolTip != 0)
                        {
                            if (iTotal > maxRowsInToolTip)
                            {
                                collapsedHint += string.Format("{0}", lastHintLine);
                                collapsedHint += string.Format("{0}", currentSnapshot.GetLineFromLineNumber(region.EndLine).GetText());
                                break;
                            }
                        }
                    }

                    if (startTokens[region.TokenIndex] == @"//region")
                        collapsedText = string.Format("# {1}", region.LineText); // region.LineText.Substring(0, region.LineText.Length - 8));
                    else
                        collapsedText = string.Format("{0} {1} {2}", startTokens[region.TokenIndex], "...", endTokens[region.TokenIndex]);

                    // the region starts at the beginning of the "[", and goes until the *end* of the line that contains the "]".
                    yield return new TagSpan<IOutliningRegionTag>(
                        new SnapshotSpan(startLine.Start + region.StartOffset, endLine.End),
                        new OutliningRegionTag(false, false, collapsedText, collapsedHint));
                }
            }
        }

        void BufferChanged(object sender, TextContentChangedEventArgs e)
        {
            // If this isn't the most up-to-date version of the buffer, then ignore it for now (we'll eventually get another change event). 
            if (e.After != buffer.CurrentSnapshot)
                return;
            this.ReParse();
        }

        void ReParse()
        {
            ITextSnapshot newSnapshot = buffer.CurrentSnapshot;
            List<Region> newRegions = new List<Region>();

            // keep the current (deepest) partial region, which will have 
            // references to any parent partial regions.
            PartialRegion currentRegion = null;

            foreach (var line in newSnapshot.Lines)
            {
                string text = line.GetText();
                string type = string.Empty;
                int regionStart = -1;
                int currentTokenIndex = -1;

                // Support for multiple tokens
                if (this.GetTextTokenIndex(text, startTokens, out regionStart, out currentTokenIndex))
                    type = "S"; // Start token
                else if (this.GetTextTokenIndex(text, endTokens, out regionStart, out currentTokenIndex))
                    type = "E"; // End token
                //else
                //    continue;

                if (type == "S")
                {
                    int currentLevel = (currentRegion != null) ? currentRegion.Level : 1;
                    int newLevel;
                    if (!TryGetLevel(text, regionStart, out newLevel))
                        newLevel = currentLevel + 1;

                    //levels are the same and we have an existing region; 
                    //end the current region and start the next 
                    if (currentLevel == newLevel && currentRegion != null)
                    {
                        newRegions.Add(new Region()
                        {
                            Level = currentRegion.Level,
                            StartLine = currentRegion.StartLine,
                            StartOffset = currentRegion.StartOffset,
                            EndLine = line.LineNumber,
                            LineText = currentRegion.LineText,
                            TokenIndex = currentTokenIndex //currentRegion.TokenIndex
                        });

                        currentRegion = new PartialRegion()
                        {
                            Level = newLevel,
                            StartLine = line.LineNumber,
                            StartOffset = regionStart,
                            PartialParent = currentRegion.PartialParent,
                            LineText = text,
                            TokenIndex = currentTokenIndex
                        };
                    }
                    else //this is a new (sub)region 
                    {
                        currentRegion = new PartialRegion()
                        {
                            Level = newLevel,
                            StartLine = line.LineNumber,
                            StartOffset = regionStart,
                            PartialParent = currentRegion,
                            LineText = text,
                            TokenIndex = currentTokenIndex
                        };
                    }
                }
                else if (type == "E")
                {
                    int currentLevel = (currentRegion != null) ? currentRegion.Level : 1;
                    int closingLevel;
                    if (!TryGetLevel(text, regionStart, out closingLevel))
                        closingLevel = currentLevel;

                    //the regions match 
                    if (currentRegion != null && 
                        currentLevel == closingLevel && 
                        currentTokenIndex == currentRegion.TokenIndex)
                    {
                        newRegions.Add(new Region()
                        {
                            Level = currentLevel,
                            StartLine = currentRegion.StartLine,
                            StartOffset = currentRegion.StartOffset,
                            LineText = currentRegion.LineText,
                            TokenIndex = currentRegion.TokenIndex,
                            EndLine = line.LineNumber
                        });
                        
                        currentRegion = currentRegion.PartialParent;
                    }
                }
            }

            //determine the changed span, and send a changed event with the new spans
            List<Span> oldSpans = new List<Span>(this.regions.Select(r => AsSnapshotSpan(r, this.snapshot)
                                                        .TranslateTo(newSnapshot, SpanTrackingMode.EdgeExclusive)
                                                        .Span));
            List<Span> newSpans = new List<Span>(newRegions.Select(r => AsSnapshotSpan(r, newSnapshot).Span));

            NormalizedSpanCollection oldSpanCollection = new NormalizedSpanCollection(oldSpans);
            NormalizedSpanCollection newSpanCollection = new NormalizedSpanCollection(newSpans);

            //the changed regions are regions that appear in one set or the other, but not both.
            NormalizedSpanCollection removed = NormalizedSpanCollection.Difference(oldSpanCollection, newSpanCollection);

            int changeStart = int.MaxValue;
            int changeEnd = -1;

            if (removed.Count > 0)
            {
                changeStart = removed[0].Start;
                changeEnd = removed[removed.Count - 1].End;
            }

            if (newSpans.Count > 0)
            {
                changeStart = Math.Min(changeStart, newSpans[0].Start);
                changeEnd = Math.Max(changeEnd, newSpans[newSpans.Count - 1].End);
            }

            this.snapshot = newSnapshot;
            this.regions = newRegions;

            if (changeStart <= changeEnd)
            {
                ITextSnapshot snap = this.snapshot;
                if (this.TagsChanged != null)
                    this.TagsChanged(this, new SnapshotSpanEventArgs(new SnapshotSpan(this.snapshot, Span.FromBounds(changeStart, changeEnd))));
            }
        }

        static bool TryGetLevel(string text, int startIndex, out int level)
        {
            level = -1;
            if (text.Length > startIndex + 3)
            {
                if (int.TryParse(text.Substring(startIndex + 1), out level))
                    return true;
            }

            return false;
        }


        static SnapshotSpan AsSnapshotSpan(Region region, ITextSnapshot snapshot)
        {
            var startLine = snapshot.GetLineFromLineNumber(region.StartLine);
            var endLine = (region.StartLine == region.EndLine)
                            ? startLine
                            : snapshot.GetLineFromLineNumber(region.EndLine);

            return new SnapshotSpan(startLine.Start + region.StartOffset, endLine.End);
        }

        private bool GetTextTokenIndex(string text, string[] tokens, out int regionStart, out int tokenIndex)
        {
            regionStart = -1;
            tokenIndex = 0;

            foreach (string token in tokens)
            {
                regionStart = text.IndexOf(token, StringComparison.Ordinal);
                if (regionStart != -1)
                    return true;

                tokenIndex++;
            }

            return false;
        }

    }
}
