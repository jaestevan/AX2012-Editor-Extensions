using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

/// <summary>
/// Serializable helper for System.Windows.Media.Color
/// </summary>
[Serializable]
public struct Colour
{
    public byte A;
    public byte R;
    public byte G;
    public byte B;

    public Colour(byte a, byte r, byte g, byte b)
    {
        A = a;
        R = r;
        G = g;
        B = b;
    }

    public Colour(Color color) : this(color.A, color.R, color.G, color.B)
    {
    }

    public static implicit operator Colour(Color color)
    {
        return new Colour(color);
    }

    public static implicit operator Color(Colour colour)
    {
        return Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
    }
}