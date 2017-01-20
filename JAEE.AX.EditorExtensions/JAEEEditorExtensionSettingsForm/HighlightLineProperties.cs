using System.ComponentModel;
using System.Drawing;

namespace JAEE.AX.EditorExtensions
{
    internal class HighlightLineProperties
    {
        protected Color backColor;
        protected Color frameColor;
        protected double opacity;

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

        [
           DisplayName("Background opacity factor"),
           DefaultValue(0.3),
           Description("Default opacity to the background color (1 = 100%)"),
           Category("Appearance"),
        ]
        public double BackOpacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        private HighlightLineProperties() 
        { 
        
        }

        public HighlightLineProperties(JAEECurrentLineHighlightSettings settings)
        {
            this.BackColor = settings.BackColor;
            this.FrameColor = settings.FrameColor;
            this.BackOpacity = settings.BackOpacity;
        }
    }
}
