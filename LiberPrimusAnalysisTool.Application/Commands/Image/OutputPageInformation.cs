using LiberPrimusAnalysisTool.Application.Queries.Page;
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
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="mediator">The mediator.</param>
            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            /// <summary>
            /// Handles the specified request.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var pages = await _mediator.Send(new GetPages.Query(true));

                foreach (var page in pages)
                {
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
                }
            }
        }
    }
}