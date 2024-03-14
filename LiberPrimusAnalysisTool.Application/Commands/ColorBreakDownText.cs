using ImageMagick;
using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Character;
using LiberPrimusAnalysisTool.Utility.Logging;
using MediatR;
using Microsoft.Extensions.Configuration;
using Nest;
using System.Text;

namespace LiberPrimusAnalysisTool.Application.Commands
{
    /// <summary>
    /// ColorBreakDownText
    /// </summary>
    public class ColorBreakDownText
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
        public class Handler : INotificationHandler<Command>
        {
            /// <summary>
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// The logging utility
            /// </summary>
            private readonly ILoggingUtility _loggingUtility;

            /// <summary>
            /// The elastic client
            /// </summary>
            private readonly ElasticClient _elasticClient;

            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            /// <param name="loggingUtility">The logging utility.</param>
            /// <param name="configuration">The configuration.</param>
            /// <param name="mediator">The mediator.</param>
            public Handler(ICharacterRepo characterRepo, ILoggingUtility loggingUtility, IConfiguration configuration, IMediator mediator)
            {
                var settings = new ConnectionSettings(new Uri($"http://{configuration["ElkServer"]}:{configuration["ElkPort"]}"));
                settings.DefaultIndex("colorbreakdowntext");

                _elasticClient = new ElasticClient(settings);
                _characterRepo = characterRepo;
                _loggingUtility = loggingUtility;
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var files = await _mediator.Send(new GetPages.Command());

                Parallel.ForEach(files, file =>
                {
                    _loggingUtility.Log($"Processing {file.PageName}");

                    using (var imageFromFile = new MagickImage(file.FileName))
                    {
                        var pixels = imageFromFile.GetPixels();
                        string currentColor = string.Empty;
                        int pixelCounter = 0;
                        List<ColorArray> colorList = new List<ColorArray>();

                        _loggingUtility.Log($"Document: {file} and Pixel Colors Count By Color");
                        foreach (Pixel pixel in pixels)
                        {
                            if (!colorList.Any(x => x.Color == pixel.ToColor().ToHexString()))
                            {
                                colorList.Add(new ColorArray(pixel.ToColor().ToHexString(), file.PageName));
                            }
                        }

                        _loggingUtility.Log($"Document: {file} and Processing Pixel Colors Count By Color");
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

                        _loggingUtility.Log($"Document: {file} and Converting To Text");
                        foreach (var color in colorList)
                        {
                            StringBuilder asciiSb = new StringBuilder();
                            foreach (var item in color.Lengths)
                            {
                                try
                                {
                                    asciiSb.Append(_characterRepo.GetASCIICharFromDec(item));
                                }
                                catch { }
                            }

                            StringBuilder ansiSb = new StringBuilder();
                            foreach (var item in color.Lengths)
                            {
                                try
                                {
                                    ansiSb.Append(_characterRepo.GetANSICharFromDec(item));
                                }
                                catch { }
                            }

                            color.ASCIICharArray = asciiSb.ToString();
                            color.ANSICharArray = ansiSb.ToString();

                            _elasticClient.IndexDocument<ColorArray>(color);
                        }
                    }
                });
            }
        }
    }
}