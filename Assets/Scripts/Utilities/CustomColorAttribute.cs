using System;
using UnityEngine;

public class CustomColorAttribute : Attribute
{
    public byte Red { get; private set; }
    public byte Green { get; private set; }
    public byte Blue { get; private set; }
    public byte Alpha { get; private set; }
    public string HexadecimalColor { get; private set; }

    /// <summary>
    /// Called when a class is created without parameters.
    /// </summary>
    public CustomColorAttribute() { }

    /// <summary>
    /// Called when a class is created with Color parameters.
    /// </summary>
    public CustomColorAttribute(byte red, byte green, byte blue, byte alpha = 35)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    /// <summary>
    /// Called when a class is created with a hexadecimal parameter.
    /// </summary>
    public CustomColorAttribute(string hexadecimalColor)
    {
        HexadecimalColor = hexadecimalColor;
    }

    /// <summary>
    /// Create a color through rgba parameters.
    /// </summary>
    /// <returns>Returns a Color32.</returns>
    public Color32 GetColor()
    {
        return new Color32(Red, Green, Blue, Alpha);
    }

    /// <summary>
    /// Create a color through hexadecimal code.
    /// </summary>
    /// <returns></returns>
    public Color32 HexadecimalToRGBColor()
    {
        Color color;
        ColorUtility.TryParseHtmlString(HexadecimalColor, out color);
        return (Color32)color;
    }
}
