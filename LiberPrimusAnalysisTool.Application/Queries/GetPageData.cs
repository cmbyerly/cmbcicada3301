using ImageMagick;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Logging;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace LiberPrimusAnalysisTool.Application.Queries
{
    /// <summary>
    /// Indexes the liber primus pages into the database.
    /// </summary>
    public class GetPageData
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : MediatR.IRequest<LiberPage>
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
        public class Handler : IRequestHandler<Command, LiberPage>
        {
            /// <summary>
            /// The logging utility
            /// </summary>
            private readonly ILoggingUtility _loggingUtility;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="loggingUtility">The logging utility.</param>
            /// <param name="configuration">The configuration.</param>
            public Handler(ILoggingUtility loggingUtility, IConfiguration configuration)
            {
                _loggingUtility = loggingUtility;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<LiberPage> Handle(Command request, CancellationToken cancellationToken)
            {
                LiberPage page;
                var files = Directory.EnumerateFiles("./liber-primus__images--full").ToList();

                await _loggingUtility.Log($"Processing {request.PageId}");
                var file = files.Where(x => x.Contains(request.PageId)).First();
                using (var imageFromFile = new MagickImage(file))
                {
                    page = new LiberPage
                    {
                        FileName = file,
                        PageName = request.PageId,
                        PageSig = imageFromFile.Signature,
                        TotalColors = imageFromFile.TotalColors,
                        Height = imageFromFile.Height,
                        Width = imageFromFile.Width
                    };
                }

                return page;
            }
        }
    }
}