using ImageMagick;
using LiberPrimusAnalysisTool.Application.Commands.Algo;
using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Application.Queries.Math;
using LiberPrimusAnalysisTool.Application.Queries.Page;
using LiberPrimusAnalysisTool.Entity;
using MediatR;
using SixLabors.ImageSharp.PixelFormats;
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
                            var tmpPage = await _mediator.Send(new GetPageData.Query() { PageId = selection });
                            liberPages.Add(tmpPage);
                        }
                    }

                    // Now we need to select the sequence to use
                    var sequenceSelection = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[green]Please select sequence to use[/]:")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more sequences)[/]")
                            .AddChoices(new[]
                            {
                                "0: All Pixels (Natural Sequence)",
                                "1: Winnow by Prime",
                                "2: Winnow by Fibonacci",
                                "3: Winnow by Totient",
                            }));

                    var choice = sequenceSelection.Split(":")[0];

                    List<int> sequence = new List<int>();
                    switch (choice)
                    {
                        case "0":
                            for (int i = 0; i <= liberPages[0].PixelCount; i++)
                            {
                                sequence.Add(i);
                            }
                            break;

                        case "1":
                            var tmpPrimeList = await _mediator.Send(new GetPrimeSequence.Query() { Number = liberPages[0].PixelCount });
                            sequence = tmpPrimeList.ToList();
                            break;

                        case "2":
                            var tmpFibList = await _mediator.Send(new GetFibonacciSequence.Query() { MaxNumber = liberPages[0].PixelCount });
                            sequence = tmpFibList.ToList();
                            break;

                        case "3":
                            var totient = await _mediator.Send(new GetTotientSequence.Query() { Number = liberPages[0].PixelCount });
                            sequence = totient.Sequence;
                            break;

                        default:
                            break;
                    }

                    // Getting the pixels from the sequence
                    AnsiConsole.WriteLine("Getting pixels from sequence");
                    List<Tuple<LiberPage, List<System.Drawing.Color>>> pixelData = new List<Tuple<LiberPage, List<System.Drawing.Color>>>();
                    Parallel.ForEach(liberPages, page =>
                    {
                        AnsiConsole.WriteLine($"Sequencing {page.PageName}");
                        using (var imageFromFile = new MagickImage(page.FileName))
                        {
                            var pixels = imageFromFile.GetPixels();
                            List<System.Drawing.Color> tmpPixelList = new List<System.Drawing.Color>();

                            foreach (var item in sequence)
                            {
                                System.Drawing.Color color = ColorTranslator.FromHtml(pixels.ElementAt(item).ToColor().ToHexString().ToUpper());
                                AnsiConsole.WriteLine($"Sequencing {page.PageName} Pixel {item}");
                                tmpPixelList.Add(color);
                            }

                            pixelData.Add(new Tuple<LiberPage, List<System.Drawing.Color>>(page, tmpPixelList));
                        }
                    });

                    // Now we need to select the image processing algorithm
                    var imageProcessingSelection = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("[green]Please select image processing algorithm[/]:")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more algorithms)[/]")
                            .AddChoices(new[]
                            {
                                "1: RGB",
                            }));

                    choice = imageProcessingSelection.Split(":")[0];

                    switch (choice)
                    {
                        case "1":
                            await _mediator.Publish(new ProcessRGB.Command() { PixelData = pixelData });
                            break;

                        default:
                            break;
                    }

                    returnToMenu = AnsiConsole.Confirm("Return to main menu?");
                }
            }
        }
    }
}