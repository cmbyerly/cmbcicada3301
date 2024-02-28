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

                    List<string> colors = new List<string>();

                    Console.WriteLine($"Document: {file} and Pixel Colors Count By Color");
                    foreach (Pixel pixel in pixels)
                    {
                        if (!colors.Any(x => x == pixel.ToColor().ToHexString()))
                        {
                            colors.Add(pixel.ToColor().ToHexString());
                        }
                    }

                    Console.WriteLine($"Document: {file} and Processing Pixel Colors Count By Color");
                    bool record = true;
                    int pixelCounter = 1;
                    StringBuilder pixelCounterString = new StringBuilder();
                    foreach (var color in colors)
                    {
                        Console.WriteLine($"Document: {file} and Processing {color}");
                        pixelCounterString.Append($"{color}: ");

                        foreach (Pixel pixel in pixels)
                        {
                            if (pixel.ToColor().ToHexString() != currentColor)
                            {
                                currentColor = pixel.ToColor().ToHexString();

                                if (pixel.ToColor().ToHexString() == color)
                                {
                                    pixelCounter = 1;
                                    record = true;
                                }
                                else
                                {
                                    if (record)
                                    {
                                        pixelCounterString.Append($"{pixelCounter} ");
                                        record = false;
                                    }
                                }
                            }
                            else
                            {
                                if (pixel.ToColor().ToHexString() == color)
                                {
                                    pixelCounter++;
                                    record = true;
                                }
                            }
                        }

                        contents.Add(pixelCounterString.ToString());
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

        /// <summary>
        /// Froms the gematria.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string FromGematria(int value)
        {
            string retval = string.Empty;
            switch(value)
            {
                case 2:
                    retval = "F";
                    break;
                case 3:
                    retval = "U";
                    break;
                case 5:
                    retval = "TH";
                    break;
                case 7:
                    retval = "O";
                    break;
                case 11:
                    retval = "R";
                    break;
                case 13:
                    retval = "K";
                    break;
                case 17:
                    retval = "G";
                    break;
                case 19:
                    retval = "W";
                    break;
                case 23:
                    retval = "H";
                    break;
                case 29:
                    retval = "N";
                    break;
                case 31:
                    retval = "I";
                    break;
                case 37:
                    retval = "J";
                    break;
                case 41:
                    retval = "EO";
                    break;
                case 43:
                    retval = "P";
                    break;
                case 47:
                    retval = "X";
                    break;
                case 53:
                    retval = "Z";
                    break;
                case 59:
                    retval = "T";
                    break;
                case 61:
                    retval = "B";
                    break;
                case 67:
                    retval = "E";
                    break;
                case 71:
                    retval = "M";
                    break;
                case 73:
                    retval = "L";
                    break;
                case 79:
                    retval = "ING";
                    break;
                case 83:
                    retval = "OE";
                    break;
                case 89:
                    retval = "D";
                    break;
                case 97:
                    retval = "A";
                    break;
                case 101:
                    retval = "AE";
                    break;
                case 103:
                    retval = "Y";
                    break;
                case 107:
                    retval = "IA";
                    break;
                case 109:
                    retval = "EA";
                    break;
                default:
                    break;
            }

            return retval;
        }
    }
}
