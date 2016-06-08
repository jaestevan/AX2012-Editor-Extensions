using System;
using System.Windows.Media;

namespace JAEE.AX.EditorExtensions
{
    [Serializable()]
    public class JAEECurrentLineHighlightSettings
    {
        public Colour BackColor { get; set; }
        public Colour FrameColor { get; set; }
        public double BackOpacity { get; set; }

        public JAEECurrentLineHighlightSettings()
        {

            this.BackColor = new Colour(System.Drawing.SystemColors.GradientActiveCaption);
            this.FrameColor = new Colour(System.Drawing.SystemColors.ActiveCaption);
            this.BackOpacity = 0.2;
        }
    }
}
