using System.Drawing;

namespace AsciiGenerator.Controller.Ascii;

public interface IAsciiGenerator
{
    public Image? Generate(Image original);
    public Color? HomogenizeBlockColor(Bitmap image, int x, int y);
    public Color HomogenizeColor(Color colorA, Color? colorB);
    public int CalculateAsciiByLuminance(Color? pixel);
}
