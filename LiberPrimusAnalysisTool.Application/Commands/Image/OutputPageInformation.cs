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
                List<string> csv = new List<string>();
                long counter = 0;
                long id = 1;
                foreach (var tpage in pages)
                {
                    if (File.Exists($"./output/TB_LIBER_PIXEL.{tpage.PageName}.csv"))
                    {
                        continue;
                    }

                    var page = await _mediator.Send(new GetPageData.Query(tpage.PageName, true));
                    var pageEntry = _liberContext.LiberPages.FirstOrDefault(x => x.PageName == page.PageName);
                    if (pageEntry == null)
                    {
                        AnsiConsole.WriteLine($"Indexing: {page.PageName}");
                        await _liberContext.AddAsync(page);
                        await _liberContext.SaveChangesAsync();
                    }

                    foreach (var pixel in page.Pixels)
                    {
                        pixel.Position = counter;

                        csv.Add($"{id}, {counter},{pixel.X},{pixel.Y},{pixel.R},{pixel.G},{pixel.B},{pixel.Hex},{tpage.PageName}");

                        counter++;
                        id++;
                    }

                    counter = 0;

                    using (StreamWriter file = System.IO.File.CreateText($"./output/TB_LIBER_PIXEL.{tpage.PageName}.csv"))
                    {
                        file.WriteLine("Id,POSITION,X,Y,R,G,B,HEX,PAGE_NAME");

                        foreach (var line in csv)
                        {
                            file.WriteLine(line);
                        }

                        file.Flush();
                        file.Dispose();
                    }

                    csv.Clear();
                };
            }
        }
    }
}