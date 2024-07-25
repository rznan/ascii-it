using System.Drawing;

namespace AsciiGenerator.Controller.Ascii;

[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Interoperability",
    "CA1416:Validate platform compatibility",
    Justification = "<Pending>"
)]
public class AsciiPrintController : AsciiControllerBase
{
    private char[] CHARSET = [' ', '.', ':', '-', '=', '+', '*', '#', '%', '@'];

    public AsciiPrintController()
    {
        CHARACTER_COUNT = CHARSET.Count();
    }

    public override Image? Generate(Image original)
    {
        int rowCount = (int)Math.Ceiling((double)original.Height / BLOCK_HEIGHT);
        int colCount = (int)Math.Ceiling((double)original.Width / BLOCK_WIDTH);

        using (Bitmap originalBitmap = new Bitmap(original))
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int x = j * BLOCK_WIDTH;
                    int y = i * BLOCK_HEIGHT;

                    Color? blockAverageColor = HomogenizeBlockColor(originalBitmap, x, y);
                    int characterIndex = CalculateAsciiByLuminance(blockAverageColor);
                    Console.Write(CHARSET[characterIndex]);
                    Console.Write(CHARSET[characterIndex]);
                }
                Console.WriteLine();
            }
        }
        return null;
    }
}
