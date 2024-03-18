using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;
using System.Text;

namespace LiberPrimusAnalysisTool.Application.Commands.Algo
{
    public class ProcessLSB
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

                var asciiProcessing = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[green]7-bit or 8-bit Character set[/]:")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more algorithms)[/]")
                            .AddChoices(new[]
                            {
                                "7: ASCII",
                                "8: ANSI",
                                "9: Gemetria Primus"
                            }));

                var iAsciiProcessing = Convert.ToInt32(asciiProcessing.Split(":")[0]);

                var orderOfColors = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[green]Order of Colors[/]:")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more algorithms)[/]")
                            .AddChoices(new[]
                            {
                                "RGB",
                                "RBG",
                                "GBR",
                                "GRB",
                                "BRG",
                                "BGR",
                            }));

                var bitsOfSig = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[green]Bits to gather[/]:")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more algorithms)[/]")
                            .AddChoices(new[]
                            {
                                "1",
                                "2",
                                "3",
                                "4",
                                "5",
                                "6",
                                "7",
                            }));

                int iBitsOfSig = Convert.ToInt32(bitsOfSig);

                Parallel.ForEach(request.PixelData, data =>
                {
                    AnsiConsole.WriteLine($"Getting bits from {data.Item1.PageName}");
                    StringBuilder bits = new StringBuilder();

                    char[] orderProcessing = new char[3] { orderOfColors[0], orderOfColors[1], orderOfColors[2] };

                    foreach (var pixel in data.Item2)
                    {
                        var rBits = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
                        var gBits = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
                        var bBits = Convert.ToString(pixel.B, 2).PadLeft(8, '0');

                        foreach (var order in orderProcessing)
                        {
                            switch (order)
                            {
                                case 'R':
                                    bits.Append(rBits.Substring(8 - iBitsOfSig, iBitsOfSig));
                                    break;

                                case 'G':
                                    bits.Append(gBits.Substring(8 - iBitsOfSig, iBitsOfSig));
                                    break;

                                case 'B':
                                    bits.Append(bBits.Substring(8 - iBitsOfSig, iBitsOfSig));
                                    break;
                            }
                        }   
                    }

                    AnsiConsole.WriteLine($"Building bytes for bits for {data.Item1.PageName}");
                    List<string> charBinList = new List<string>();
                    StringBuilder ascii = new StringBuilder();
                    foreach (var character in bits.ToString())
                    {
                        ascii.Append(character);
                        if (ascii.Length >= iAsciiProcessing)
                        {
                            charBinList.Add(ascii.ToString());
                            ascii.Clear();
                        }
                    }

                    AnsiConsole.WriteLine($"Filtering bytes too short {data.Item1.PageName}");
                    charBinList = charBinList.Where(x => x.Length == iAsciiProcessing).ToList();

                    AnsiConsole.WriteLine($"Building character string for {data.Item1.PageName}");
                    StringBuilder stringForFile = new StringBuilder();
                    string characterForFile;
                    foreach (var charBin in charBinList)
                    {
                        switch (iAsciiProcessing)
                        {
                            case 7:
                                characterForFile = _characterRepo.GetASCIICharFromBin(charBin, includeControlCharacters);
                                break;

                            case 8:
                                characterForFile = _characterRepo.GetANSICharFromBin(charBin, includeControlCharacters);
                                break;

                            case 9:
                                try
                                {
                                    characterForFile = _characterRepo.GetCharacterFromGematriaValue(Convert.ToInt32(charBin, 2));
                                }
                                catch(Exception e)
                                {
                                    characterForFile = string.Empty;
                                    AnsiConsole.WriteLine($"Error: {e.Message}");
                                }
                                break;

                            default:
                                characterForFile = string.Empty;
                                break;
                        }

                        stringForFile.Append(characterForFile);
                    }

                    AnsiConsole.WriteLine($"Outputting file for {data.Item1.PageName}");
                    File.WriteAllText($"./output/{data.Item1.PageName}-LSB-{request.Method}-{orderOfColors}-{iAsciiProcessing}-{iBitsOfSig}.txt", stringForFile.ToString());
                });
            }
        }
    }
}