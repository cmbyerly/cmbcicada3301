using ImageMagick;
using System.Text;

namespace LiberPrimusAnalysisTool
{
    /// <summary>
    /// Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Running Liber Primus Color Analysis Tool");

            RemovePreviousRuns();

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
                    contents.Add($"Total Colors: {imageFromFile.TotalColors}");

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
                        contents.Add(color.ToNumArraySpaced());
                        contents.Add(color.ToNumArrayNonSpaced());
                        contents.Add(color.ToGemetriaString());
                        contents.Add(color.ToCharArray());
                        contents.Add("");
                    }

                    Console.WriteLine($"Document: {file}.round2.txt Writing file");
                    WriteToFile(contents, RemoveNonAlphaNum(file) + ".round2.txt");

                    // This was the round 1 stuff.
                    //currentColor = string.Empty;
                    //int currentY = 0;
                    //List<string> pixelStrings = new List<string>();
                    //StringBuilder gemBuilder = new StringBuilder();
                    //StringBuilder numberBuilder = new StringBuilder();

                    //gemBuilder.Append($"GFN: ");
                    //numberBuilder.Append($"NUM: ");

                    //foreach (Pixel pixel in pixels)
                    //{
                    //    if (pixel.ToColor().ToHexString() != currentColor || currentY != pixel.Y)
                    //    {
                    //        currentColor = pixel.ToColor().ToHexString();
                    //        Console.WriteLine($"Document: {file} and Pixel Color: {pixel.ToColor().ToHexString()} and Y position is {currentY}");

                    //        if (pixelStrings.Count > 0)
                    //        {
                    //            numberBuilder.Append($"{pixelStrings.Count} ");
                    //            gemBuilder.Append(FromGematria(pixelStrings.Count));

                    //            if (currentY != pixel.Y)
                    //            {
                    //                WriteLineInformation(contents, gemBuilder, numberBuilder);
                    //            }
                    //        }

                    //        pixelStrings.Clear();
                    //        pixelStrings.Add(pixel.ToColor().ToHexString());
                    //        currentY = pixel.Y;
                    //    }
                    //    else
                    //    {
                    //        pixelStrings.Add(pixel.ToColor().ToHexString());
                    //    }
                    //}

                    //WriteLineInformation(contents, gemBuilder, numberBuilder);
                    //WriteToFile(contents, RemoveNonAlphaNum(file) + ".round1.txt");
                }
            });
        }

        /// <summary>
        /// Writes the line information.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="gemBuilder">The gem builder.</param>
        /// <param name="numberBuilder">The number builder.</param>
        private static void WriteLineInformation(List<string> contents, StringBuilder gemBuilder, StringBuilder numberBuilder)
        {
            contents.Add(numberBuilder.ToString());
            contents.Add(gemBuilder.ToString());
            contents.Add("");

            gemBuilder.Clear();
            numberBuilder.Clear();

            gemBuilder.Append($"GFN: ");
            numberBuilder.Append($"NUM: ");
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="file">The file.</param>
        private static void WriteToFile(IEnumerable<string> values, string file)
        {
            File.WriteAllLines($"{System.Environment.CurrentDirectory}/output/{file}", values);
        }

        /// <summary>
        /// Removes the previous runs.
        /// </summary>
        private static void RemovePreviousRuns()
        {
            
            if (!Directory.Exists($"{System.Environment.CurrentDirectory}/output"))
            {
                Directory.CreateDirectory($"{System.Environment.CurrentDirectory}/output");
            }

            var files = Directory.GetFiles($"{System.Environment.CurrentDirectory}/output");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// Removes the non alpha number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string RemoveNonAlphaNum(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in value)
            {
                if (char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
