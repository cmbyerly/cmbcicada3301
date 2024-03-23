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
            /// Initializes a new instance of the <see cref="Command" /> class.
            /// </summary>
            /// <param name="pixelData">The pixel data.</param>
            /// <param name="method">The method.</param>
            /// <param name="includeControlCharacters">if set to <c>true</c> [include control characters].</param>
            /// <param name="asciiProcessing">The ASCII processing.</param>
            /// <param name="bitsOfSig">The bits of sig.</param>
            /// <param name="colorOrder">The color order.</param>
            public Command(List<Tuple<LiberPage, List<Entity.Pixel>>> pixelData, string method, bool includeControlCharacters, int asciiProcessing, int bitsOfSig, string colorOrder)
            {
                PixelData = pixelData;
                Method = method;
                IncludeControlCharacters = includeControlCharacters;
                AsciiProcessing = asciiProcessing;
                BitsOfSig = bitsOfSig;
                ColorOrder = colorOrder;
            }

            /// <summary>
            /// Gets or sets the pixel data.
            /// </summary>
            /// <value>
            /// The pixel data.
            /// </value>
            public List<Tuple<LiberPage, List<Entity.Pixel>>> PixelData { get; set; }

            /// <summary>
            /// Gets or sets the method.
            /// </summary>
            /// <value>
            /// The method.
            /// </value>
            public string Method { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [include control characters].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [include control characters]; otherwise, <c>false</c>.
            /// </value>
            public bool IncludeControlCharacters { get; set; }

            /// <summary>
            /// Gets or sets the ASCII processing.
            /// </summary>
            /// <value>
            /// The ASCII processing.
            /// </value>
            public int AsciiProcessing { get; set; }

            /// <summary>
            /// Gets or sets the bits of sig.
            /// </summary>
            /// <value>
            /// The bits of sig.
            /// </value>
            public int BitsOfSig { get; set; }

            /// <summary>
            /// Gets or sets the color order.
            /// </summary>
            /// <value>
            /// The color order.
            /// </value>
            public string ColorOrder { get; set; }
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
                Parallel.ForEach(request.PixelData, data =>
                {
                    AnsiConsole.WriteLine($"ProcessLSB: Getting bits from {data.Item1.PageName}");
                    List<char> bits = new List<char>();

                    char[] orderProcessing = new char[3] { request.ColorOrder[0], request.ColorOrder[1], request.ColorOrder[2] };

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
                                    var tmpRbits = rBits.Substring(8 - request.BitsOfSig, request.BitsOfSig);
                                    foreach (var bit in tmpRbits)
                                    {
                                        bits.Add(bit);
                                    }
                                    break;

                                case 'G':
                                    var tmpGbits = gBits.Substring(8 - request.BitsOfSig, request.BitsOfSig);
                                    foreach (var bit in tmpGbits)
                                    {
                                        bits.Add(bit);
                                    }
                                    break;

                                case 'B':
                                    var tmpBbits = bBits.Substring(8 - request.BitsOfSig, request.BitsOfSig);
                                    foreach (var bit in tmpBbits)
                                    {
                                        bits.Add(bit);
                                    }
                                    break;
                            }
                        }
                    }

                    AnsiConsole.WriteLine($"ProcessLSB: Building bytes for bits for {data.Item1.PageName}");
                    List<string> charBinList = new List<string>();
                    StringBuilder ascii = new StringBuilder();
                    foreach (var character in bits)
                    {
                        ascii.Append(character);
                        if (ascii.Length >= request.AsciiProcessing)
                        {
                            charBinList.Add(ascii.ToString());
                            ascii.Clear();
                        }
                    }

                    AnsiConsole.WriteLine($"ProcessLSB: Filtering out nibbles for {data.Item1.PageName}");
                    charBinList = charBinList.Where(x => x.Length == request.AsciiProcessing).ToList();

                    AnsiConsole.WriteLine($"ProcessLSB: Building character string for {data.Item1.PageName}");
                    AnsiConsole.WriteLine($"ProcessLSB: Outputting file for {data.Item1.PageName}");
                    using (var file = File.CreateText($"./output/{data.Item1.PageName}-LSB-{request.Method}-{request.ColorOrder}-{request.AsciiProcessing}-{request.BitsOfSig}.txt"))
                    {
                        foreach (var charBin in charBinList)
                        {
                            switch (request.AsciiProcessing)
                            {
                                case 7:
                                    file.Write(_characterRepo.GetASCIICharFromBin(charBin, request.IncludeControlCharacters));
                                    break;

                                case 8:
                                    file.Write(_characterRepo.GetANSICharFromBin(charBin, request.IncludeControlCharacters));
                                    break;

                                case 9:
                                    try
                                    {
                                        file.Write(_characterRepo.GetCharacterFromGematriaValue(Convert.ToInt32(charBin, 2)));
                                    }
                                    catch (Exception e)
                                    {
                                        file.Write(string.Empty);
                                        AnsiConsole.WriteLine($"Error: {e.Message}");
                                    }
                                    break;

                                default:
                                    file.Write(string.Empty);
                                    break;
                            }
                        }

                        file.Flush();
                        file.Close();
                        file.Dispose();
                    }
                });
            }
        }
    }
}