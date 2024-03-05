using ImageMagick;
using LiberPrimusAnalysisTool.Utility;
using System.Text;

namespace LiberPrimusAnalysisTool.Analyzers
{
    /// <summary>
    /// This was the round one test
    /// </summary>
    public static class ColorCountText
    {
        /// <summary>
        /// Runs me.
        /// </summary>
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

                    // This was the round 1 stuff.
                    currentColor = string.Empty;
                    int currentY = 0;
                    List<string> pixelStrings = new List<string>();
                    StringBuilder gemBuilder = new StringBuilder();
                    StringBuilder numberBuilder = new StringBuilder();

                    gemBuilder.Append($"GEMNUMS: ");
                    numberBuilder.Append($"NUMBERS: ");

                    foreach (Pixel pixel in pixels)
                    {
                        if (pixel.ToColor().ToHexString() != currentColor || currentY != pixel.Y)
                        {
                            currentColor = pixel.ToColor().ToHexString();
                            Console.WriteLine($"Document: {file} and Pixel Color: {pixel.ToColor().ToHexString()} and Y position is {currentY}");

                            if (pixelStrings.Count > 0)
                            {
                                numberBuilder.Append($"{pixelStrings.Count} ");
                                gemBuilder.Append(GemetriaUtilty.FromGematria(pixelStrings.Count));

                                if (currentY != pixel.Y)
                                {
                                    WriteLineInformation(contents, gemBuilder, numberBuilder);
                                }
                            }

                            pixelStrings.Clear();
                            pixelStrings.Add(pixel.ToColor().ToHexString());
                            currentY = pixel.Y;
                        }
                        else
                        {
                            pixelStrings.Add(pixel.ToColor().ToHexString());
                        }
                    }

                    WriteLineInformation(contents, gemBuilder, numberBuilder);
                    FileUtilities.WriteToFile(contents, StringUtilities.RemoveNonAlphaNum(file) + ".round1.txt");
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
            contents.Add($"NPRIMES: {StringUtilities.StringArrayPrimes(numberBuilder.ToString())}");
            contents.Add("");

            gemBuilder.Clear();
            numberBuilder.Clear();

            gemBuilder.Append($"GEMNUMS: ");
            numberBuilder.Append($"NUMBERS: ");
        }
    }
}