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
    /// Color Isolation
    /// </summary>
    public class ColorIsolation
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
                    // Getting the pages we want
                    List<LiberPage> liberPages = new List<LiberPage>();
                    var winnowAllPages = AnsiConsole.Confirm("Isolate colors on all pages?");

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
                                                .Title("[green]Please select pages to isolate color[/]:")
                                                .PageSize(10)
                                                .MoreChoicesText("[grey](Move up and down to reveal more pages)[/]")
                                                .AddChoices(pageList.Select(x => x.PageName).ToList()));

                        foreach (var selection in pageSelection)
                        {
                            var tmpPage = await _mediator.Send(new GetPageData.Query() { PageId = selection });
                            liberPages.Add(tmpPage);
                        }
                    }

                    // Copy the images to a new directory and set colors.
                    Parallel.ForEach(liberPages, async page =>
                    {
                        var pageColors = await _mediator.Send(new GetPageColors.Query() { PageId = page.PageName });

                        foreach (var color in pageColors)
                        {
                            File.Copy(page.FileName, $"./output/{page.PageName}-{color.LiberColorHashless}.jpg", true);
                            AnsiConsole.WriteLine($"Isolating color {color.LiberColorHex} on {page.PageName}-{color.LiberColorHashless}.jpg");
                            using (var image = new MagickImage($"./output/{page.PageName}-{color.LiberColorHashless}.jpg"))
                            {
                                var pixels = image.GetPixels();
                                foreach (var pixel in pixels)
                                {
                                    if (pixel.ToColor().ToHexString() == color.LiberColorHex)
                                    {
                                        pixel.SetChannel(0, 191);
                                        pixel.SetChannel(1, 64);
                                        pixel.SetChannel(2, 191);
                                        pixels.SetPixel(pixel);
                                    }
                                }

                                image.Write($"./output/{page.PageName}-{color.LiberColorHashless}.jpg");
                            }
                        }
                    });

                    returnToMenu = AnsiConsole.Confirm("Return to main menu?");
                }
            }
        }
    }
}