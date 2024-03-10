using ImageMagick;
using LiberPrimusAnalysisTool.Database.DBInterfaces;
using LiberPrimusAnalysisTool.Entity.Old;
using LiberPrimusAnalysisTool.Utility;
using LiberPrimusAnalysisTool.Utility.Math;
using MediatR;

namespace LiberPrimusAnalysisTool.Analyzers
{
    /// <summary>
    /// ColorReport
    /// </summary>
    public class ColorReport
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : INotification
        {
        }

        /// <summary>
        ///Handler
        /// </summary>
        /// <seealso cref="MediatR.IRequestHandler&lt;LiberPrimusAnalysisTool.Analyzers.ColorReport.Command&gt;" />
        public class Handler : INotificationHandler<ColorReport.Command>
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

                        LoggingUtility.Log($"Document: {file} and Pixel Colors Count By Color");
                        foreach (Pixel pixel in pixels)
                        {
                            if (!colorList.Any(x => x.Color == pixel.ToColor().ToHexString()))
                            {
                                colorList.Add(new ColorArray(pixel.ToColor().ToHexString()));
                            }
                        }

                        LoggingUtility.Log($"Document: {file} and Processing Pixel Colors Count By Color");
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

                        LoggingUtility.Log($"Document: {file} and Converting To Text");
                        foreach (var color in colorList)
                        {
                            contents.Add(color.ColorCount());
                            contents.Add(color.ToPrimeArray());
                            contents.Add("");
                        }

                        LoggingUtility.Log($"Document: {file}.round3.txt Writing file");
                        FileUtilities.WriteToFile(contents, StringUtilities.RemoveNonAlphaNum(file) + ".round3.txt");
                    }
                });
            }
        }
    }
}