using Microsoft.VisualStudio.Text.Tagging;

namespace JAEE.AX.EditorExtensions
{
    internal class HighlightWordTag : TextMarkerTag
    {
        public HighlightWordTag() : base("MarkerFormatDefinition/HighlightWordFormatDefinition") { }
    }
}
