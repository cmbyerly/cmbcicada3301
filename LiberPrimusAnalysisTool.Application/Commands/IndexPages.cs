using ImageMagick;
using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Character;
using LiberPrimusAnalysisTool.Utility.Logging;
using MediatR;
using Microsoft.Extensions.Configuration;
using Nest;

namespace LiberPrimusAnalysisTool.Application.Commands
{
    /// <summary>
    /// ColorReport
    /// </summary>
    public class IndexPages
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : INotification
        {
        }

        /// <summary>
        ///Handler
        /// </summary>
        /// <seealso cref="MediatR.IRequestHandler&lt;LiberPrimusAnalysisTool.Analyzers.ColorReport.Command&gt;" />
        public class Handler : INotificationHandler<IndexPages.Command>
        {
            /// <summary>
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// The logging utility
            /// </summary>
            private readonly ILoggingUtility _loggingUtility;

            /// <summary>
            /// The elastic client
            /// </summary>
            private readonly ElasticClient _elasticClient;

            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            /// <param name="loggingUtility">The logging utility.</param>
            /// <param name="configuration">The configuration.</param>
            /// <param name="mediator">The mediator.</param>
            public Handler(ICharacterRepo characterRepo, ILoggingUtility loggingUtility, IConfiguration configuration, IMediator mediator)
            {
                var settings = new ConnectionSettings(new Uri($"http://{configuration["ElkServer"]}:{configuration["ElkPort"]}"));
                settings.DefaultIndex("pageindex");
                _elasticClient = new ElasticClient(settings);
                _characterRepo = characterRepo;
                _loggingUtility = loggingUtility;
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var files = await _mediator.Send(new GetPages.Command());

                Parallel.ForEach(files, async file =>
                {
                    var colors =await _mediator.Send(new GetPageColors.Command { PageId = file.PageName });
                    file.Colors = colors.ToList();
                    await _loggingUtility.Log($"Processing {file}");
                    _elasticClient.IndexDocument<LiberPage>(file);
                });
            }
        }
    }
}