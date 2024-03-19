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

                foreach(var tpage in pages)
                {
                    var page = await _mediator.Send(new GetPageData.Query(tpage.PageName, true));

                    var pageEntry = _liberContext.LiberPages.FirstOrDefault(x => x.PageName == page.PageName);
                    if (pageEntry == null)
                    {
                        AnsiConsole.WriteLine($"Indexing: {page.PageName}");
                        await _liberContext.AddAsync(page);
                        await _liberContext.SaveChangesAsync();
                    }

                    long counter = 0;
                    foreach (var pixel in page.Pixels)
                    {
                        pixel.Position = counter;
                        counter++;
                    }

                    await _liberContext.AddRangeAsync(page.Pixels.ToArray());
                    await _liberContext.SaveChangesAsync();

                    AnsiConsole.WriteLine($"Writing pixel for: {page.PageName} - {counter}");
                };
            }
        }
    }
}