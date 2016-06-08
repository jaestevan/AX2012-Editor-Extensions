using System;
using System.Drawing;

namespace JAEE.AX.EditorExtensions
{
    [Serializable()]
    public class JAEEHighlightWordSettings
    {
        public Color BackColor { get; set; }
        public Color FrameColor { get; set; }

        public JAEEHighlightWordSettings()
        {
            this.BackColor = Color.LightGray;
            this.FrameColor = Color.LightGray;
        }
    }
}
