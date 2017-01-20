using System;
using System.Drawing;

namespace JAEE.AX.EditorExtensions
{
    [Serializable()]
    public class JAEEBraceMatchingSettings
    {
        public Color BackColor { get; set; }
        public Color FrameColor { get; set; }

        public JAEEBraceMatchingSettings()
        {
            this.BackColor = Color.Silver;
            this.FrameColor = Color.Black;
        }

    }
}