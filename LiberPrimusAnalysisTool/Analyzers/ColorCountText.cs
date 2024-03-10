using ImageMagick;
using LiberPrimusAnalysisTool.Database.DBInterfaces;
using LiberPrimusAnalysisTool.Utility;
using MediatR;
using System.Text;

namespace LiberPrimusAnalysisTool.Analyzers
{
    /// <summary>
    /// This was the round one test
    /// </summary>
    public class ColorCountText
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : INotification
        {
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : INotificationHandler<ColorCountText.Command>
        {
            /// <summary>
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            public Handler(ICharacterRepo characterRepo)
            {
                _characterRepo = characterRepo;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var files = Directory.EnumerateFiles("./liber-primus__images--full");

                Parallel.ForEach(files, file =>
                {
                    LoggingUtility.Log($"Processing {file}");

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
                                LoggingUtility.Log($"Document: {file} and Pixel Color: {pixel.ToColor().ToHexString()} and Y position is {currentY}");

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
            /// <param name="gemBuilder">The gem builder.</param>
            /// <param name="numberBuilder">The number builder.</param>
            private void WriteLineInformation(List<string> contents, StringBuilder gemBuilder, StringBuilder numberBuilder)
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
}