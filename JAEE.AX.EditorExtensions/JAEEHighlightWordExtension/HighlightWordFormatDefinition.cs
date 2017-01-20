using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace JAEE.AX.EditorExtensions
{
    [Export(typeof(EditorFormatDefinition))]
    [Name("MarkerFormatDefinition/HighlightWordFormatDefinition")]
    [UserVisible(true)]
    internal class HighlightWordFormatDefinition : MarkerFormatDefinition
    {
        public HighlightWordFormatDefinition()
        {
            this.DisplayName = "Highlight Selected Word";
            this.ZOrder = 5;

            this.loadSettings();
        }

        private void loadSettings()
        {
            EditorSettings settings = EditorSettings.getInstance();

            this.BackgroundColor = System.Windows.Media.Color.FromRgb(settings.HighlightWord.BackColor.R, settings.HighlightWord.BackColor.G, settings.HighlightWord.BackColor.B);
            this.ForegroundColor = System.Windows.Media.Color.FromRgb(settings.HighlightWord.FrameColor.R, settings.HighlightWord.FrameColor.G, settings.HighlightWord.FrameColor.B);
        }
    }
}
