using CommandLine;

namespace AsciiGenerator.Model;

public class Options
{
    [Option('i', "input", Required = true, HelpText = "Path to the input image")]
    public string input { get; set; } = string.Empty;

    [Option('o', "output", Required = false, HelpText = "Path to the desired output")]
    public string? output { get; set; }

    [Option('p', "print", Required = false, HelpText = "To print the result to the terminal")]
    public bool print { get; set; }
}
