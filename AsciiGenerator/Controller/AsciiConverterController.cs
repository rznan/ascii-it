using System.Drawing;

namespace AsciiGenerator.Controller;

public class AsciiConverterController
{
    private readonly string SPRITE_SHEET_PATH = "./Resources/spritesheet.bmp";
    private readonly int CHARACTER_WIDTH = 8; // pixels
    private readonly int CHARACTER_HEIGHT = 8; // pixels
    private readonly int CHARACTER_COUNT;

    private readonly Image SPRITE_SHEET;
    private readonly Rectangle[] PARTS;


    public AsciiConverterController()
    {
        SPRITE_SHEET = new Bitmap(SPRITE_SHEET_PATH);
        CHARACTER_COUNT = SPRITE_SHEET.Width / CHARACTER_WIDTH;
        PARTS = new Rectangle[CHARACTER_COUNT];
        for (int i = 0; i < CHARACTER_COUNT; i++)
        {
            PARTS[i] = new Rectangle(i * CHARACTER_WIDTH, 0, CHARACTER_WIDTH, CHARACTER_HEIGHT);
        }
    }

    // pular de inicio a inicio dos blocos de pixels e chamar AverageBlockColor
    // passar o resultado para 
    public Image GenerateAsciiImage(Image original)
    {
        int rowAmmount = (int)Math.Ceiling((double)original.Height / CHARACTER_HEIGHT);
        int colAmmount = (int)Math.Ceiling((double)original.Width / CHARACTER_WIDTH);

        Bitmap result = new Bitmap(colAmmount * CHARACTER_WIDTH, rowAmmount * CHARACTER_HEIGHT);

        using (Bitmap originalBitmap = new Bitmap(original))
        using (Graphics g = Graphics.FromImage(result))
        {
            for (int i = 0; i < rowAmmount; i++)
            {
                for (int j = 0; j < colAmmount; j++)
                {
                    int x = j * CHARACTER_WIDTH;
                    int y = i * CHARACTER_HEIGHT;

                    int partNumber = GetPartNumberByLuminance(AverageBlockColor(originalBitmap, x, y));

                    Rectangle ascciCharacter = PARTS[partNumber];
                    Rectangle destination = new Rectangle(x, y, CHARACTER_WIDTH, CHARACTER_WIDTH);

                    g.DrawImage(
                        SPRITE_SHEET,
                        destination,
                        ascciCharacter,
                        GraphicsUnit.Pixel
                    );

                }
            }
        }

        return result;
    }


    private Color? AverageBlockColor(Bitmap image, int x, int y)
    {
        int width = image.Width;
        int height = image.Height;

        // tracks the pointer's deviation from it's original position
        int x_offset = 0;
        int y_offset = 0;

        Color? averageColor = null;

        while (y_offset < CHARACTER_HEIGHT && y < height)
        {
            while (x_offset < CHARACTER_WIDTH && x < width)
            {
                var pixel_color = image.GetPixel(x, y);
                averageColor = AverageColors(pixel_color, averageColor);

                x_offset++;
                x++;
            }
            y_offset++;
            y++;
        }

        return averageColor;
    }


    private static Color AverageColors(Color colorA, Color? colorB)
    {
        if (colorB == null) return colorA;

        return Color.FromArgb(
            red: (colorA.R + colorB.Value.R) / 2,
            green: (colorA.G + colorB.Value.G) / 2,
            blue: (colorA.B + colorB.Value.B) / 2
        );
    }


    private int GetPartNumberByLuminance(Color? pixel)
    {
        if (pixel == null)
        {
            return 0;
        }
        float luminance = pixel.Value.GetBrightness();
        return (int)(luminance * (CHARACTER_COUNT - 1));
    }
}
