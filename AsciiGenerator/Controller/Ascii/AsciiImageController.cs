using System.Drawing;

namespace AsciiGenerator.Controller.Ascii;


[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Interoperability",
    "CA1416:Validate platform compatibility",
    Justification = "<Pending>"
)]
public class AsciiImageController : AsciiControllerBase
{
    private readonly string SPRITE_SHEET_PATH = "./Resources/spritesheet.bmp";
    private readonly Image SPRITE_SHEET;
    private readonly Rectangle[] PARTS;


    public AsciiImageController()
    {
        SPRITE_SHEET = new Bitmap(SPRITE_SHEET_PATH);
        CHARACTER_COUNT = SPRITE_SHEET.Width / BLOCK_WIDTH;
        PARTS = new Rectangle[CHARACTER_COUNT];
        for (int i = 0; i < CHARACTER_COUNT; i++)
        {
            PARTS[i] = new Rectangle(i * BLOCK_WIDTH, 0, BLOCK_WIDTH, BLOCK_HEIGHT);
        }
    }

    public override Image? Generate(Image original)
    {
        int rowCount = (int)Math.Ceiling((double)original.Height / BLOCK_HEIGHT);
        int colCount = (int)Math.Ceiling((double)original.Width / BLOCK_WIDTH);

        Bitmap resultImage = new Bitmap(colCount * BLOCK_WIDTH, rowCount * BLOCK_HEIGHT);

        using (Bitmap originalBitmap = new Bitmap(original))
        using (Graphics g = Graphics.FromImage(resultImage))
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int x = j * BLOCK_WIDTH;
                    int y = i * BLOCK_HEIGHT;

                    Color? blockAverageColor = HomogenizeBlockColor(originalBitmap, x, y);
                    int partNumber = CalculateAsciiByLuminance(blockAverageColor);

                    Rectangle ascciCharacter = PARTS[partNumber];
                    Rectangle destination = new Rectangle(x, y, BLOCK_WIDTH, BLOCK_WIDTH);

                    g.DrawImage(
                        SPRITE_SHEET,
                        destination,
                        ascciCharacter,
                        GraphicsUnit.Pixel
                    );

                }
            }
        }

        return resultImage;
    }
}
