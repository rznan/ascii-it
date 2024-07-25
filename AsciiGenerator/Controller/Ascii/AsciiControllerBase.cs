using System.Drawing;

namespace AsciiGenerator.Controller.Ascii;

[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Interoperability",
    "CA1416:Validate platform compatibility",
    Justification = "<Pending>"
)]
public abstract class AsciiControllerBase : IAsciiGenerator
{
    internal int BLOCK_WIDTH = 8; // pixels
    internal int BLOCK_HEIGHT = 8; // pixels
    internal int CHARACTER_COUNT;


    public abstract Image? Generate(Image original);


    public Color? HomogenizeBlockColor(Bitmap image, int x, int y)
    {
        int width = image.Width;
        int height = image.Height;

        // tracks the pointer's deviation from it's original position
        int x_offset = 0;
        int y_offset = 0;

        Color? averageColor = null;

        while (y_offset < BLOCK_HEIGHT && y < height)
        {
            while (x_offset < BLOCK_WIDTH && x < width)
            {
                var pixel_color = image.GetPixel(x, y);
                averageColor = HomogenizeColor(pixel_color, averageColor);

                x_offset++;
                x++;
            }
            y_offset++;
            y++;
        }

        return averageColor;
    }


    public Color HomogenizeColor(Color colorA, Color? colorB)
    {
        if (colorB == null) return colorA;

        return Color.FromArgb(
            red: (colorA.R + colorB.Value.R) / 2,
            green: (colorA.G + colorB.Value.G) / 2,
            blue: (colorA.B + colorB.Value.B) / 2
        );
    }


    public int CalculateAsciiByLuminance(Color? pixel)
    {
        if (pixel == null)
        {
            return 0;
        }
        float luminance = pixel.Value.GetBrightness();
        return (int)(luminance * (CHARACTER_COUNT - 1));
    }

}
