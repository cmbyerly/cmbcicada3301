﻿using LiberPrimusAnalysisTool.Application.Commands.PixelProcessing;
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
    public class PixelWinnowPages
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
                    Console.Clear();
                    AnsiConsole.Write(new FigletText("Winnow By Pixels").Centered().Color(Color.Green));

                    // Getting the pages we want to winnow
                    List<LiberPage> liberPages = new List<LiberPage>();

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
                    var invertPixels = AnsiConsole.Confirm("Reverse Pixels?", false);
                    var shiftSequence = AnsiConsole.Confirm("Shift sequence down by one?", false);

                    // Getting the page data.
                    foreach (var selection in pageSelection)
                    {
                        var tmpPage = await _mediator.Send(new GetPageData.Query(selection, true, invertPixels));
                        liberPages.Add(tmpPage);
                    }

                    string seqtext = string.Empty;

                    for (int i = 0; i <= 3; i++)
                    {
                        List<long> sequence = new List<long>();
                        switch (i)
                        {
                            case 0:
                                long n = -1;
                                seqtext = invertPixels ? "ReversedPix-Natural" : "Natural";
                                sequence = liberPages[0].Pixels.Select(x => { n++; return n; }).ToList();
                                break;

                            case 1:
                                seqtext = invertPixels ? "ReversedPix-Prime" : "Prime";
                                var tmpPrimeList = await _mediator.Send(new GetPrimeSequence.Query() { Number = liberPages[0].PixelCount });
                                sequence = tmpPrimeList.ToList();
                                break;

                            case 2:
                                seqtext = invertPixels ? "ReversedPix-Fib" : "Fib";
                                var tmpFibList = await _mediator.Send(new GetFibonacciSequence.Query() { MaxNumber = liberPages[0].PixelCount });
                                sequence = tmpFibList.ToList();
                                break;

                            case 3:
                                seqtext = invertPixels ? "ReversedPix-Totient" : "Totient";
                                var totient = await _mediator.Send(new GetTotientSequence.Query() { Number = liberPages[0].PixelCount });
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
                        AnsiConsole.WriteLine($"Getting pixels from sequence {seqtext}");
                        List<Tuple<LiberPage, List<Entity.Pixel>>> pixelData = new List<Tuple<LiberPage, List<Entity.Pixel>>>();
                        Parallel.ForEach(liberPages, page =>
                        {
                            AnsiConsole.WriteLine($"Sequencing {page.PageName}");
                            List<Entity.Pixel> tmpPixelList = new List<Entity.Pixel>();
                            foreach (var seq in sequence)
                            {
                                if (shiftSequence && !seqtext.Contains("Natural"))
                                {
                                    tmpPixelList.Add(page.Pixels.ElementAt((int)seq - 1));
                                }
                                else
                                {
                                    tmpPixelList.Add(page.Pixels.ElementAt((int)seq));
                                }
                            }
                            pixelData.Add(new Tuple<LiberPage, List<Entity.Pixel>>(page, tmpPixelList));

                            AnsiConsole.WriteLine($"Sequenced {page.PageName}");
                        });

                        GC.Collect();

                        for (int p = 0; p <= 3; p++)
                        {
                            switch (p)
                            {
                                case 1:
                                    await _mediator.Publish(new ProcessRGB.Command(pixelData, seqtext, includeControlCharacters));
                                    break;

                                case 2:
                                    foreach (var asciiProcessing in new List<int>() { 7, 8, 9 })
                                    {
                                        foreach (var bitsOfSig in new List<int>() { 1, 2, 3, 4, 5, 6, 7 })
                                        {
                                            foreach (var colorOrder in new List<string>() { "RGB", "RBG", "GBR", "GRB", "BRG", "BGR" })
                                            {
                                                await _mediator.Publish(new ProcessLSB.Command(pixelData, seqtext, includeControlCharacters, asciiProcessing, bitsOfSig, colorOrder));
                                            }
                                        }
                                    }
                                    break;

                                case 3:
                                    foreach (var bitsOfSig in new List<int>() { 1, 2, 3, 4, 5, 6, 7 })
                                    {
                                        foreach (var colorOrder in new List<string>() { "RGB", "RBG", "GBR", "GRB", "BRG", "BGR" })
                                        {
                                            await _mediator.Publish(new ProcessToBytes.Command(pixelData, seqtext, bitsOfSig, colorOrder));
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    returnToMenu = AnsiConsole.Confirm("Return to main menu?");
                }
            }
        }
    }
}