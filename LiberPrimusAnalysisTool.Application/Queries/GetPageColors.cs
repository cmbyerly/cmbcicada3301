using ImageMagick;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Logging;
using MediatR;
using Nest;

namespace LiberPrimusAnalysisTool.Application.Queries
{
    /// <summary>
    /// Index Colors
    /// </summary>
    public class GetPageColors
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : MediatR.IRequest<IEnumerable<LiberColor>>
        {
            /// <summary>
            /// Gets or sets the page identifier.
            /// </summary>
            /// <value>
            /// The page identifier.
            /// </value>
            public string PageId { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Command, IEnumerable<LiberColor>>
        {
            /// <summary>
            /// The elastic client
            /// </summary>
            private readonly ElasticClient _elasticClient;

            /// <summary>
            /// The logging utility
            /// </summary>
            private readonly ILoggingUtility _loggingUtility;

            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="loggingUtility">The logging utility.</param>
            /// <param name="mediator">The mediator.</param>
            public Handler(ILoggingUtility loggingUtility, IMediator mediator)
            {
                _loggingUtility = loggingUtility;
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task<IEnumerable<LiberColor>> Handle(Command request, CancellationToken cancellationToken)
            {
                List<LiberColor> colors = new List<LiberColor>();

                var page = await _mediator.Send(new GetPageData.Command { PageId = request.PageId });

                await _loggingUtility.Log($"Getting colors for {page.FileName}");

                using (var imageFromFile = new MagickImage(page.FileName))
                {
                    var pixels = imageFromFile.GetPixels();
                    var pixColors = pixels.Select(x => x.ToColor().ToHexString()).Distinct().ToList();

                    foreach (var color in pixColors)
                    {
                        var liberColor = new LiberColor
                        {
                            LiberColorHex = color
                        };

                        await _loggingUtility.Log($"Processing: {page} - Getting: {liberColor}");
                    }
                }

                return colors;
            }
        }
    }
}