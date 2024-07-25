using System.Drawing;
using CommandLine;
using AsciiGenerator.Controller.Ascii;
using AsciiGenerator.Model;

[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Interoperability",
    "CA1416:Validate platform compatibility",
    Justification = "<Pending>"
)]
public class Program
{
    public static void Main(string[] args)
    {
        if (ValidateEnvironment())
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(options => RunApp(options));
        }
    }

    private static void RunApp(Options opts)
    {
        IAsciiGenerator generator = opts.print ? new AsciiPrintController() : new AsciiImageController();

        int result = opts.print
            ? GenerateAsciiArt(opts.input, generator)
            : string.IsNullOrEmpty(opts.output)
                ? GenerateAsciiArt(opts.input, generator)
                : GenerateAsciiArt(opts.input, generator, opts.output);

        Environment.Exit(result);
    }

    private static int GenerateAsciiArt(string imagePath, IAsciiGenerator generator, string output = "result.jpg")
    {
        try
        {
            using Image img = Image.FromFile(imagePath);
            using Image? asciiImage = generator.Generate(img);
            if (asciiImage != null)
            {
                asciiImage.Save(output);
                Console.WriteLine($"Generated at {output}");
            }
        }
        catch (FileNotFoundException)
        {
            return HandleError($"Could not find Image: {imagePath}", 404);
        }
        catch (OutOfMemoryException)
        {
            return HandleError($"Image format not supported: {imagePath}", 400);
        }
        catch (System.Runtime.InteropServices.ExternalException)
        {
            return HandleError($"Could not save image as: {output}", 400);
        }

        return 0;
    }

    private static int HandleError(string message, int errorCode)
    {
        Console.Error.WriteLine(message);
        return errorCode;
    }

    private static bool ValidateEnvironment()
    {
        if (!OperatingSystem.IsWindows())
        {
            Console.Error.WriteLine("Supported OS: Windows");
            return false;
        }

        return true;
    }
}
