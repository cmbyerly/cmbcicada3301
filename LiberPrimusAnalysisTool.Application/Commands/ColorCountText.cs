using ImageMagick;
using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility;
using LiberPrimusAnalysisTool.Utility.Character;
using LiberPrimusAnalysisTool.Utility.Logging;
using LiberPrimusAnalysisTool.Utility.Math;
using MediatR;
using Microsoft.Extensions.Configuration;
using Nest;

namespace LiberPrimusAnalysisTool.Application.Commands
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
                settings.DefaultIndex("colorcounttext");
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
                var files = await _mediator.Send(new GetPages.Command(true));

                Parallel.ForEach(files, file =>
                {
                    _loggingUtility.Log($"Processing {file}");

                    using (var imageFromFile = new MagickImage(file.FileName))
                    {
                        var pixels = imageFromFile.GetPixels();
                        string currentColor = string.Empty;

                        // This was the round 1 stuff.
                        currentColor = string.Empty;
                        int currentY = 0;
                        List<string> pixelStrings = new List<string>();
                        var lineColorInfo = new LineColorInfo();

                        foreach (Pixel pixel in pixels)
                        {
                            if (pixel.ToColor().ToHexString() != currentColor || currentY != pixel.Y)
                            {
                                currentColor = pixel.ToColor().ToHexString();
                                _loggingUtility.Log($"Document: {file} and Pixel Color: {pixel.ToColor().ToHexString()} and Y position is {currentY}");

                                if (pixelStrings.Count > 0)
                                {
                                    lineColorInfo = new LineColorInfo
                                    {
                                        LineNumber = currentY,
                                        LiberColor = new LiberColor { LiberColorHex = currentColor },
                                        LiberPage = file,
                                        LineOrientation = "Horizontal",
                                        LineColorCount = pixelStrings.Count,
                                        IsPrime = PrimeUtility.IsPrime(pixelStrings.Count),
                                        GemetriaValue = GemetriaUtilty.FromGematria(pixelStrings.Count),
                                        CharacterValue = _characterRepo.GetANSICharFromDec(pixelStrings.Count, true)
                                    };

                                    _elasticClient.IndexDocument(lineColorInfo);
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

                        lineColorInfo = new LineColorInfo
                        {
                            LineNumber = currentY,
                            LiberColor = new LiberColor { LiberColorHex = currentColor },
                            LiberPage = file,
                            LineOrientation = "Horizontal",
                            LineColorCount = pixelStrings.Count,
                            IsPrime = PrimeUtility.IsPrime(pixelStrings.Count),
                            GemetriaValue = GemetriaUtilty.FromGematria(pixelStrings.Count),
                            CharacterValue = _characterRepo.GetANSICharFromDec(pixelStrings.Count, true)
                        };

                        _elasticClient.IndexDocument(lineColorInfo);
                    }
                });
            }
        }
    }
}