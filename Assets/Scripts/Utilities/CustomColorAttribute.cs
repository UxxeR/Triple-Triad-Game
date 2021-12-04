using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColorAttribute : Attribute
{
    public byte Red { get; private set; }
    public byte Green { get; private set; }
    public byte Blue { get; private set; }
    public byte Alpha { get; private set; }
    public string HexadecimalColor { get; private set; }

    public CustomColorAttribute() { }

    public CustomColorAttribute(byte red, byte green, byte blue, byte alpha = 35)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public CustomColorAttribute(string hexadecimalColor)
    {
        HexadecimalColor = hexadecimalColor;
    }

    public Color32 GetColor()
    {
        return new Color32(Red, Green, Blue, Alpha);
    }

    public Color32 HexadecimalToRGBColor()
    {
        Color color;
        ColorUtility.TryParseHtmlString(HexadecimalColor, out color);
        return (Color32)color;
    }
}
