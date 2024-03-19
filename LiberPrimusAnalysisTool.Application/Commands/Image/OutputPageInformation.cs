using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Application.Queries.Page;
using LiberPrimusAnalysisTool.Database;
using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Image
{
    /// <summary>
    /// Output Page Information
    /// </summary>
    public class OutputPageInformation
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
            /// The liber context
            /// </summary>
            private readonly LiberContext _liberContext;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="mediator">The mediator.</param>
            public Handler(IMediator mediator)
            {
                _mediator = mediator;
                _liberContext = new LiberContext();
                _liberContext.Database.EnsureCreated();
            }

            /// <summary>
            /// Handles the specified request.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var pages = await _mediator.Send(new GetPages.Query(false));

                foreach (var tpage in pages)
                {
                    var page = await _mediator.Send(new GetPageData.Query(tpage.PageName, true));

                    AnsiConsole.WriteLine($"Page: {page.PageName}");
                    AnsiConsole.WriteLine($"Image: {page.FileName}");
                    AnsiConsole.WriteLine($"Pixel Count: {page.PixelCount}");
                    AnsiConsole.WriteLine($"Total Colors: {page.TotalColors}");
                    AnsiConsole.WriteLine($"Width: {page.Width}");
                    AnsiConsole.WriteLine($"Height: {page.Height}");
                    AnsiConsole.WriteLine($"Signature: {page.PageSig}");
                    AnsiConsole.WriteLine();

                    File.AppendAllText("./output/pageinfo.txt", $"Page: {page.PageName}" + Environment.NewLine);
                    File.AppendAllText("./output/pageinfo.txt", $"Image: {page.FileName}" + Environment.NewLine);
                    File.AppendAllText("./output/pageinfo.txt", $"Pixel Count: {page.PixelCount}" + Environment.NewLine);
                    File.AppendAllText("./output/pageinfo.txt", $"Total Colors: {page.TotalColors}" + Environment.NewLine);
                    File.AppendAllText("./output/pageinfo.txt", $"Width: {page.Width}" + Environment.NewLine);
                    File.AppendAllText("./output/pageinfo.txt", $"Height: {page.Height}" + Environment.NewLine);
                    File.AppendAllText("./output/pageinfo.txt", $"Signature: {page.PageSig}" + Environment.NewLine);
                    File.AppendAllText("./output/pageinfo.txt", string.Empty + Environment.NewLine);

                    var pageEntry = _liberContext.LiberPages.FirstOrDefault(x => x.PageName == page.PageName);
                    if (pageEntry == null)
                    {
                        AnsiConsole.WriteLine($"Indexing: {page.PageName}");
                        _liberContext.Add(page);
                        _liberContext.SaveChanges();
                    }

                    var counter = 0;
                    foreach (var pixel in page.Pixels)
                    {
                        var pixelEntry = _liberContext.Pixels.FirstOrDefault(x => x.Position == counter && x.PageName == page.PageName);
                        if (pixelEntry == null)
                        {
                            AnsiConsole.WriteLine($"Writing pixel for: {page.PageName} - {counter}");
                            pixel.Position = counter;
                            _liberContext.Add(pixel);
                            _liberContext.SaveChanges();
                        }
                        counter++;
                    }
                }
            }
        }
    }
}