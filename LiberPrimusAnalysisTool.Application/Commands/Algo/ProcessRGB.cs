using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;

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
            /// Initializes a new instance of the <see cref="Command"/> class.
            /// </summary>
            /// <param name="pixelData">The pixel data.</param>
            /// <param name="method">The method.</param>
            public Command(List<Tuple<LiberPage, List<System.Drawing.Color>>> pixelData, string method)
            {
                PixelData = pixelData;
                Method = method;
            }

            /// <summary>
            /// Gets or sets the pixel data.
            /// </summary>
            /// <value>
            /// The pixel data.
            /// </value>
            public List<Tuple<LiberPage, List<System.Drawing.Color>>> PixelData { get; set; }

            /// <summary>
            /// Gets or sets the method.
            /// </summary>
            /// <value>
            /// The method.
            /// </value>
            public string Method { get; set; }
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
                    {
                        ;
                        rgbIndex.AddR(_characterRepo.GetASCIICharFromDec(pixel.R, includeControlCharacters));
                        rgbIndex.AddG(_characterRepo.GetASCIICharFromDec(pixel.G, includeControlCharacters));
                        rgbIndex.AddB(_characterRepo.GetASCIICharFromDec(pixel.B, includeControlCharacters));
                    }

                    AnsiConsole.WriteLine($"Red Text: {rgbIndex.R}");
                    AnsiConsole.WriteLine($"Green Text: {rgbIndex.G}");
                    AnsiConsole.WriteLine($"Blue Text: {rgbIndex.B}");

                    AnsiConsole.WriteLine($"Writing: ./output/{data.Item1.PageName}-RGB.txt");
                    File.AppendAllText($"./output/{data.Item1.PageName}-{request.Method}-RGB-red.txt", rgbIndex.R);
                    File.AppendAllText($"./output/{data.Item1.PageName}-{request.Method}-RGB-green.txt", rgbIndex.G);
                    File.AppendAllText($"./output/{data.Item1.PageName}-{request.Method}-RGB-blue.txt", rgbIndex.B);
                });
            }
        }
    }
}