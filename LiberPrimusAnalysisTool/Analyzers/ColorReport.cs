using ImageMagick;
using LiberPrimusAnalysisTool.Entity.Old;
using LiberPrimusAnalysisTool.Utility;
using LiberPrimusAnalysisTool.Utility.Math;

namespace LiberPrimusAnalysisTool.Analyzers
{
    public static class ColorReport
    {
        public static void RunMe()
        {
            var files = Directory.EnumerateFiles("./liber-primus__images--full");

            Parallel.ForEach(files, file =>
            {
                Console.WriteLine($"Processing {file}");

                List<string> contents = new List<string>();

                using (var imageFromFile = new MagickImage(file))
                {
                    contents.Add($"Channels {imageFromFile.Channels.Count()}");
                    contents.Add($"Height {imageFromFile.Height}");
                    contents.Add($"Width {imageFromFile.Width}");
                    contents.Add($"Comment Text: {imageFromFile.Comment}");
                    contents.Add($"Format: {imageFromFile.Format}");
                    contents.Add($"Density X: {imageFromFile.Density.X}");
                    contents.Add($"Density Y: {imageFromFile.Density.Y}");
                    contents.Add($"Density Units: {imageFromFile.Density.Units}");
                    contents.Add($"Label: {imageFromFile.Label}");
                    contents.Add($"Signature: {imageFromFile.Signature}");
                    contents.Add($"Total Colors: {imageFromFile.TotalColors} - Is Prime: {PrimeUtility.IsPrime(imageFromFile.TotalColors)}");

                    var profile = imageFromFile.GetExifProfile();

                    if (profile != null)
                    {
                        var exifCount = 12;
                        foreach (var value in profile.Values)
                        {
                            contents.Add($"{value.Tag}({value.DataType}): {value.ToString()}");
                            exifCount++;
                        }
                    }
                    else
                    {
                        contents.Add($"No Exif data");
                    }

                    contents.Add("");
                    contents.Add("");

                    var pixels = imageFromFile.GetPixels();
                    string currentColor = string.Empty;
                    int pixelCounter = 0;
                    List<ColorArray> colorList = new List<ColorArray>();

                    Console.WriteLine($"Document: {file} and Pixel Colors Count By Color");
                    foreach (Pixel pixel in pixels)
                    {
                        if (!colorList.Any(x => x.Color == pixel.ToColor().ToHexString()))
                        {
                            colorList.Add(new ColorArray(pixel.ToColor().ToHexString()));
                        }
                    }

                    Console.WriteLine($"Document: {file} and Processing Pixel Colors Count By Color");
                    foreach (Pixel pixel in pixels)
                    {
                        if (pixel.ToColor().ToHexString() != currentColor)
                        {
                            if (!string.IsNullOrEmpty(currentColor) && !string.IsNullOrWhiteSpace(currentColor))
                            {
                                colorList.First(x => x.Color == currentColor).Lengths.Add(pixelCounter);
                            }

                            currentColor = pixel.ToColor().ToHexString();
                            pixelCounter = 1;
                        }
                        else
                        {
                            pixelCounter++;
                        }
                    }

                    colorList.First(x => x.Color == currentColor).Lengths.Add(pixelCounter);

                    Console.WriteLine($"Document: {file} and Converting To Text");
                    foreach (var color in colorList)
                    {
                        contents.Add(color.ColorCount());
                        contents.Add(color.ToPrimeArray());
                        contents.Add("");
                    }

                    Console.WriteLine($"Document: {file}.round3.txt Writing file");
                    FileUtilities.WriteToFile(contents, StringUtilities.RemoveNonAlphaNum(file) + ".round3.txt");
                }
            });
        }
    }
}