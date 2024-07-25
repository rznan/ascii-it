

using System.Drawing;
using AsciiGenerator.Controller;

string imagePath = ".\\Resources\\target.jpg";

Image img = Image.FromFile(imagePath);

var asciiController = new AsciiConverterController();

Image result = asciiController.GenerateAsciiImage(img);
result.Save("Output/result.jpg");

