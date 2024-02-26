using ClosedXML.Excel;
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

            var workbook = new XLWorkbook();

            foreach (var file in files)
            {
                Console.WriteLine($"Processing {file}");

                var worksheet = workbook.Worksheets.Add(RemoveNonAlphaNum(file));

                var imageFromFile = new MagickImage(file);

                worksheet.Cell(1, 1).Value = $"Channels {imageFromFile.Channels.Count()}";
                worksheet.Cell(1, 2).Value = $"Height {imageFromFile.Height}";
                worksheet.Cell(1, 3).Value = $"Width {imageFromFile.Width}";
                worksheet.Cell(1, 4).Value = $"Comment Text: {imageFromFile.Comment}";
                worksheet.Cell(1, 5).Value = $"Format: {imageFromFile.Format}";
                worksheet.Cell(1, 6).Value = $"Density X: {imageFromFile.Density.X}";
                worksheet.Cell(1, 7).Value = $"Density Y: {imageFromFile.Density.Y}";
                worksheet.Cell(1, 8).Value = $"Density Units: {imageFromFile.Density.Units}";
                worksheet.Cell(1, 9).Value = $"Label: {imageFromFile.Label}";
                worksheet.Cell(1, 10).Value = $"Signature: {imageFromFile.Signature}";
                worksheet.Cell(1, 11).Value = $"Total Colors: {imageFromFile.TotalColors}";

                var profile = imageFromFile.GetExifProfile();

                if (profile != null)
                {
                    var exifCount = 12;
                    foreach (var value in profile.Values)
                    {
                        worksheet.Cell(1, exifCount).Value = $"{value.Tag}({value.DataType}): {value.ToString()}";
                        exifCount++;
                    }
                }
                else
                {
                    worksheet.Cell(1, 12).Value = $"No Exif data";
                }

                var pixels = imageFromFile.GetPixels();
                string currentColor = string.Empty;
                int currentY = 0;
                int xcounter = 4;
                List<string> pixelStrings = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder gemBuilder = new StringBuilder();

                foreach(Pixel pixel in pixels)
                {
                    var tmpColor = pixel.ToColor().ToHexString();

                    if (tmpColor != currentColor || currentY != pixel.Y)
                    {
                        currentColor = tmpColor;
                        Console.WriteLine($"Document: {file} and Pixel Color: {tmpColor} and Y position is {currentY} and X position is {xcounter}");

                        if (pixelStrings.Count > 0)
                        {
                            worksheet.Cell(currentY + 2, xcounter).Value = $"{pixelStrings.Count}";

                            try
                            {
                                gemBuilder.Append(FromGematria(pixelStrings.Count));
                                stringBuilder.Append(Convert.ToChar(pixelStrings.Count));
                            }
                            catch { }

                            xcounter++;

                            if (currentY != pixel.Y)
                            {
                                xcounter = 4;
                                worksheet.Cell(currentY + 2, 1).Value = stringBuilder.ToString();
                                worksheet.Cell(currentY + 2, 2).Value = gemBuilder.ToString();

                                stringBuilder.Clear();
                                gemBuilder.Clear();
                            }
                        }

                        pixelStrings.Clear();
                        pixelStrings.Add(tmpColor);
                        currentY = pixel.Y;
                    }
                    else
                    {
                        pixelStrings.Add(tmpColor);
                    }
                }

                worksheet.Cell(currentY + 2, xcounter).Value = $"{pixelStrings.Count}";
                worksheet.Cell(currentY + 2, 1).Value = stringBuilder.ToString();
                worksheet.Cell(currentY + 2, 2).Value = gemBuilder.ToString();

                stringBuilder.Clear();
                gemBuilder.Clear();

                imageFromFile = null;
            }

            workbook.SaveAs($"Run{DateTime.Now.ToString("yyyyMMdd-HHmmssffff")}.xlsx");
        }

        /// <summary>
        /// Removes the previous runs.
        /// </summary>
        private static void RemovePreviousRuns()
        {
            var files = Directory.GetFiles(".");
            foreach (var file in files) 
            { 
                if (file.EndsWith(".xlsx"))
                {
                    File.Delete(file);
                }
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
