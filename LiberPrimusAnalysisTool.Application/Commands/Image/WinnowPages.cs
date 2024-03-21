using ImageMagick;
using LiberPrimusAnalysisTool.Application.Commands.Algo;
using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Application.Queries.Math;
using LiberPrimusAnalysisTool.Application.Queries.Page;
using LiberPrimusAnalysisTool.Entity;
using MediatR;
using Spectre.Console;
using System.Drawing;

namespace LiberPrimusAnalysisTool.Application.Commands.Image
{
    /// <summary>
    /// Winnow Pages
    /// </summary>
    public class WinnowPages
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
                    // Getting the pages we want to winnow
                    List<LiberPage> liberPages = new List<LiberPage>();
                    var winnowAllPages = AnsiConsole.Confirm("Winnow all pages?");

                    if (winnowAllPages)
                    {
                        var tmpPages = await _mediator.Send(new GetPages.Query(true));
                        liberPages = tmpPages.ToList();
                    }
                    else
                    {
                        var pageList = await _mediator.Send(new GetPages.Query(false));

                        var pageSelection = AnsiConsole.Prompt(
                                                new MultiSelectionPrompt<string>()
                                                .Title("[green]Please select pages to winnow[/]:")
                                                .PageSize(10)
                                                .MoreChoicesText("[grey](Move up and down to reveal more pages)[/]")
                                                .AddChoices(pageList.Select(x => x.PageName).ToList()));

                        foreach (var selection in pageSelection)
                        {
                            var tmpPage = await _mediator.Send(new GetPageData.Query(selection, false));
                            liberPages.Add(tmpPage);
                        }
                    }

                    var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Include control characters?[/]?")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Yes",
                        "No"
                    }));

                    var includeControlCharacters = selecttion == "Yes";

                    string seqtext = string.Empty;

                    for (int i = 1; i <= 3; i++)
                    {
                        List<int> sequence = new List<int>();
                        switch (i)
                        {
                            case 0:
                                seqtext = "Natural";
                                for (int n = 0; n <= liberPages[0].PixelCount; n++)
                                {
                                    sequence.Add(n);
                                }
                                break;

                            case 1:
                                seqtext = "Prime";
                                var tmpPrimeList = await _mediator.Send(new GetPrimeSequence.Query() { Number = liberPages[0].PixelCount });
                                sequence = tmpPrimeList.ToList();
                                break;

                            case 2:
                                seqtext = "Fib";
                                var tmpFibList = await _mediator.Send(new GetFibonacciSequence.Query() { MaxNumber = liberPages[0].PixelCount });
                                sequence = tmpFibList.ToList();
                                break;

                            case 3:
                                seqtext = "Totient";
                                var totient = await _mediator.Send(new GetTotientSequence.Query() { Number = liberPages[0].PixelCount });
                                sequence = totient.Sequence;
                                break;

                            default:
                                break;
                        }

                        // Getting the pixels from the sequence
                        AnsiConsole.WriteLine($"Getting pixels from sequence {seqtext}");
                        List<Tuple<LiberPage, List<System.Drawing.Color>>> pixelData = new List<Tuple<LiberPage, List<System.Drawing.Color>>>();
                        Parallel.ForEach(liberPages, page =>
                        {
                            AnsiConsole.WriteLine($"Sequencing {page.PageName}");
                            using (var imageFromFile = new MagickImage(page.FileName))
                            using (var pixels = imageFromFile.GetPixels())
                            {
                                List<System.Drawing.Color> tmpPixelList = new List<System.Drawing.Color>();
                                foreach (var seq in sequence)
                                {
                                    var pixelColor = ColorTranslator.FromHtml(pixels.ElementAt(seq).ToColor().ToHexString().ToUpper());
                                    tmpPixelList.Add(pixelColor);
                                }
                                pixelData.Add(new Tuple<LiberPage, List<System.Drawing.Color>>(page, tmpPixelList));

                                AnsiConsole.WriteLine($"Sequenced {page.PageName}");

                                pixels.Dispose();
                            }
                        });

                        GC.Collect();

                        for (int p = 0; p <= 2; p++)
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