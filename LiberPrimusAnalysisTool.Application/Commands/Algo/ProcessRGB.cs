using ImageMagick;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;
using System.Drawing;

namespace LiberPrimusAnalysisTool.Application.Commands.Algo
{
    public class ProcessRGB
    {/// <summary>
     /// Command
     /// </summary>
     /// <seealso cref="IRequest" />
        public class Command : INotification
        {
            /// <summary>
            /// Gets or sets the pixel data.
            /// </summary>
            /// <value>
            /// The pixel data.
            /// </value>
            public List<Tuple<LiberPage, List<System.Drawing.Color>>> PixelData { get; set; }
        }

        /// <summary>
        ///Handler
        /// </summary>
        /// <seealso cref="IRequestHandler&lt;LiberPrimusAnalysisTool.Analyzers.ColorReport.Command&gt;" />
        public class Handler : INotificationHandler<Command>
        {
            /// <summary>
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            /// <param name="mediator">The mediator.</param>
            public Handler(ICharacterRepo characterRepo, IMediator mediator)
            {
                _characterRepo = characterRepo;
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Include control characters?[/]?")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Yes",
                        "No"
                    }));

                var includeControlCharacters = selecttion == "Yes";

                Parallel.ForEach(request.PixelData, data =>
                {
                    AnsiConsole.WriteLine($"Processing {data.Item1.PageName}");
                    var rgbIndex = new RgbCharacters(data.Item1.PageName);

                    AnsiConsole.WriteLine($"Document: {data.Item1.PageName} - RGB Breakdown");
                    foreach (var pixel in data.Item2)
                    {;
                        rgbIndex.AddR(_characterRepo.GetASCIICharFromDec(pixel.R, includeControlCharacters));
                        rgbIndex.AddG(_characterRepo.GetASCIICharFromDec(pixel.G, includeControlCharacters));
                        rgbIndex.AddB(_characterRepo.GetASCIICharFromDec(pixel.B, includeControlCharacters));
                    }

                    AnsiConsole.WriteLine($"Red Text: {rgbIndex.R}");
                    AnsiConsole.WriteLine($"Green Text: {rgbIndex.G}");
                    AnsiConsole.WriteLine($"Blue Text: {rgbIndex.B}");

                    AnsiConsole.WriteLine($"Writing: ./output/RgbIndex_{data.Item1.PageName}.txt");
                    File.AppendAllText($"./output/RgbIndex_{data.Item1.PageName}.txt", "Red Text:" + rgbIndex.R + Environment.NewLine + Environment.NewLine);
                    File.AppendAllText($"./output/RgbIndex_{data.Item1.PageName}.txt", "Green Text: " + rgbIndex.G + Environment.NewLine + Environment.NewLine);
                    File.AppendAllText($"./output/RgbIndex_{data.Item1.PageName}.txt", "Blue Text: " + rgbIndex.B + Environment.NewLine + Environment.NewLine);
                });
            }
        }
    }
}