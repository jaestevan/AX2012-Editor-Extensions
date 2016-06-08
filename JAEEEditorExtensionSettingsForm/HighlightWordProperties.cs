using System.ComponentModel;
using System.Drawing;

namespace JAEE.AX.EditorExtensions
{
    internal class HighlightWordProperties
    {
        protected Color backColor = Color.Transparent;
        protected Color frameColor = Color.White;

        [
            DisplayName("Background Color"),
            DefaultValue(typeof(Color), "Transparent"),
            Description("Set the color to use as the back color for highlighted words"),
            Category("Appearance"),
        ]
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        [
            DisplayName("Frame Color"),
            DefaultValue(typeof(Color), "White"),
            Description("Set the color to use as a frame color for highlighted words"),
            Category("Appearance"),
        ]
        public Color FrameColor
        {
            get { return frameColor; }
            set { frameColor = value; }
        }

        private HighlightWordProperties() 
        { 
        
        }

        public HighlightWordProperties(JAEEHighlightWordSettings settings)
        {
            this.BackColor = settings.BackColor;
            this.FrameColor = settings.FrameColor;
        }
    }
}
