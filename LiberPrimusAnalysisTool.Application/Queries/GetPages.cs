using ImageMagick;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Logging;
using MediatR;
using Microsoft.Extensions.Configuration;
using Nest;

namespace LiberPrimusAnalysisTool.Application.Queries
{
    /// <summary>
    /// Indexes the liber primus pages into the database.
    /// </summary>
    public class GetPages
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : MediatR.IRequest<IEnumerable<LiberPage>>
        {
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Command, IEnumerable<LiberPage>>
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
            public async Task<IEnumerable<LiberPage>> Handle(Command request, CancellationToken cancellationToken)
            {
                List<LiberPage> pages = new List<LiberPage>();
                var files = Directory.EnumerateFiles("./liber-primus__images--full").ToList();

                List<string> pageIds = new List<string>();
                for (int i = 0; i < 75; i++)
                {
                    pageIds.Add(i.ToString().PadLeft(2, '0'));
                }

                foreach (var pageId in pageIds)
                {
                    await _loggingUtility.Log($"Processing {pageId}");
                    var file = files.Where(x => x.Contains(pageId)).First();
                    using (var imageFromFile = new MagickImage(file))
                    {
                        var page = new LiberPage
                        {
                            FileName = file,
                            PageName = pageId,
                            PageSig = imageFromFile.Signature,
                            TotalColors = imageFromFile.TotalColors,
                            Height = imageFromFile.Height,
                            Width = imageFromFile.Width
                        };

                        pages.Add(page);
                    }
                }

                return pages;
            }
        }
    }
}