using System.Windows.Media;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Classification;

namespace JAEE.AX.EditorExtensions
{
    #region Adornment Factory

    static class CurrentLineClassificationDefinition
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(JAEECurrentLineHighlight.NAME)]
        internal static ClassificationTypeDefinition CurrentLineClassificationType = null;
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = JAEECurrentLineHighlight.NAME)]
    [Name(JAEECurrentLineHighlight.NAME)]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    sealed class CurrentLineFormat : ClassificationFormatDefinition
    {
        public CurrentLineFormat()
        {
            this.LoadSettings();
        }

        private void LoadSettings()
        {
            EditorSettings settings = EditorSettings.getInstance();

            this.BackgroundColor = Color.FromArgb(settings.HighlightCurrentLine.BackColor.A,
                                                  settings.HighlightCurrentLine.BackColor.R,
                                                  settings.HighlightCurrentLine.BackColor.G,
                                                  settings.HighlightCurrentLine.BackColor.B);
            this.ForegroundColor = Color.FromArgb(settings.HighlightCurrentLine.FrameColor.A,
                                                  settings.HighlightCurrentLine.FrameColor.R,
                                                  settings.HighlightCurrentLine.FrameColor.G,
                                                  settings.HighlightCurrentLine.FrameColor.B);
            this.BackgroundOpacity = settings.HighlightCurrentLine.BackOpacity;
            //this.ForegroundBrush = Brushes.White;
        }
    }

    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class JAEECurrentLineHighlightFactory : IWpfTextViewCreationListener
    {
        [Import]
        public IClassificationTypeRegistryService ClassificationRegistry = null;
        [Import]
        public IClassificationFormatMapService FormatMapService = null;

        [Export(typeof(AdornmentLayerDefinition))]
        [Name(JAEECurrentLineHighlight.NAME)]
        [Order(Before = "Selection")]
        [TextViewRole(PredefinedTextViewRoles.Document)]
        public AdornmentLayerDefinition editorAdornmentLayer = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            IClassificationType classification = ClassificationRegistry.GetClassificationType(JAEECurrentLineHighlight.NAME);
            IClassificationFormatMap map = FormatMapService.GetClassificationFormatMap(textView);
            new JAEECurrentLineHighlight(textView, map, classification);
        }
    }

    #endregion //Adornment Factory
}
