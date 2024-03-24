using LiberPrimusAnalysisTool.Application.Commands.ByteProcessing;
using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Application.Queries.Math;
using LiberPrimusAnalysisTool.Entity;
using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Image
{
    /// <summary>
    /// Winnow Pages
    /// </summary>
    public class ByteWinnowPages
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.INotification" />
        public class Command : INotification
        {
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : INotificationHandler<Command>
        {
            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="mediator">The mediator.</param>
            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a notification
            /// </summary>
            /// <param name="notification">The notification</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command notification, CancellationToken cancellationToken)
            {
                bool returnToMenu = false;

                while (!returnToMenu)
                {
                    var groupSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Please select page group to run[/]:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more groups)[/]")
                    .AddChoices(new[] {
                        "00",
                        "01",
                        "02",
                        "03,04",
                        "05",
                        "06,07,08,09",
                        "10,11,12,13",
                        "14,15,16",
                        "17,18,19",
                        "20,21,22,23,24",
                        "25,26,27,28,29,30,31",
                        "32,33,34,35,36,37,38",
                        "39",
                        "40",
                        "41,42,43",
                        "44,45,46,47,48,49",
                        "50,51,52,53,54,55,56",
                        "57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72",
                        "73",
                        "74",
                    }));

                    var pageSelection = groupSelection.Split(",");

                    var includeControlCharacters = AnsiConsole.Confirm("Include control characters?", false);
                    var reverseBytes = AnsiConsole.Confirm("Reverse Bytes?", false);
                    var shiftSequence = AnsiConsole.Confirm("Shift sequence down by one?", false);

                    // Getting the page data.
                    foreach (var selection in pageSelection)
                    {
                        var liberPage = await _mediator.Send(new GetPageData.Query(selection, false, false));

                        string seqtext = string.Empty;

                        for (int i = 0; i <= 3; i++)
                        {
                            List<long> sequence = new List<long>();
                            switch (i)
                            {
                                case 0:
                                    seqtext = reverseBytes ? "ReversedPix-Natural" : "Natural";
                                    for (int n = 0; n < liberPage.Bytes.Count; n++)
                                    {
                                        sequence.Add(n);
                                    }
                                    break;

                                case 1:
                                    seqtext = reverseBytes ? "ReversedPix-Prime" : "Prime";
                                    var tmpPrimeList = await _mediator.Send(new GetPrimeSequence.Query() { Number = liberPage.Bytes.Count });
                                    sequence = tmpPrimeList.ToList();
                                    break;

                                case 2:
                                    seqtext = reverseBytes ? "ReversedPix-Fib" : "Fib";
                                    var tmpFibList = await _mediator.Send(new GetFibonacciSequence.Query() { MaxNumber = liberPage.Bytes.Count });
                                    sequence = tmpFibList.ToList();
                                    break;

                                case 3:
                                    seqtext = reverseBytes ? "ReversedPix-Totient" : "Totient";
                                    var totient = await _mediator.Send(new GetTotientSequence.Query() { Number = liberPage.Bytes.Count });
                                    sequence = totient.Sequence;
                                    break;

                                default:
                                    break;
                            }

                            if (shiftSequence)
                            {
                                seqtext = "ShiftedSeq-" + seqtext;
                            }

                            // Getting the pixels from the sequence
                            AnsiConsole.WriteLine($"Getting bytes from sequence {seqtext}");

                            AnsiConsole.WriteLine($"Sequencing {liberPage.PageName}");
                            List<byte> tmpPixelList = new List<byte>();
                            List<byte> fileBytes = liberPage.Bytes;

                            if (reverseBytes)
                            {
                                fileBytes.Reverse();
                            }

                            foreach (var seq in sequence)
                            {
                                if (shiftSequence && !seqtext.Contains("Natural"))
                                {
                                    tmpPixelList.Add(fileBytes.ElementAt((int)seq - 1));
                                }
                                else
                                {
                                    tmpPixelList.Add(fileBytes.ElementAt((int)seq));
                                }
                            }

                            Tuple<LiberPage, List<byte>> pixelData = new Tuple<LiberPage, List<byte>>(liberPage, tmpPixelList);

                            AnsiConsole.WriteLine($"Sequenced {liberPage.PageName}");

                            GC.Collect();

                            for (int p = 0; p <= 1; p++)
                            {
                                switch (p)
                                {
                                    case 0:
                                        foreach (var asciiProcessing in new List<int>() { 7, 8, 9 })
                                        {
                                            foreach (var bitsOfSig in new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 })
                                            {
                                                await _mediator.Publish(new ProcessBytesLSB.Command(pixelData, seqtext, includeControlCharacters, asciiProcessing, bitsOfSig));
                                            }
                                        }
                                        break;

                                    case 1:
                                        foreach (var bitsOfSig in new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 })
                                        {
                                            await _mediator.Publish(new ProcessBytesToBytes.Command(pixelData, seqtext, bitsOfSig));
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    returnToMenu = AnsiConsole.Confirm("Return to main menu?");
                }
            }
        }
    }
}