using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Application.Queries.Page;
using LiberPrimusAnalysisTool.Database;
using LiberPrimusAnalysisTool.Utility.Character;
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
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="mediator">The mediator.</param>
            /// <param name="characterRepo">The character repo.</param>
            public Handler(IMediator mediator, ICharacterRepo characterRepo)
            {
                _mediator = mediator;
                _liberContext = new LiberContext();
                _liberContext.Database.EnsureCreated();
                _characterRepo = characterRepo;
            }

            /// <summary>
            /// Handles the specified request.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText("Init Database").Centered().Color(Color.Green));

                // initing the pages to the database.
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

                    var page = await _mediator.Send(new GetPageData.Query(tpage.PageName, false, false));
                    var pageEntry = _liberContext.LiberPages.FirstOrDefault(x => x.PageName == page.PageName);
                    if (pageEntry == null)
                    {
                        AnsiConsole.WriteLine($"Indexing: {page.PageName}");
                        await _liberContext.AddAsync(page);
                        await _liberContext.SaveChangesAsync();
                    }

                    csv.Clear();
                };
            }
        }
    }
}