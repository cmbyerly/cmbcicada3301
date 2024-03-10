using ImageMagick;
using LiberPrimusAnalysisTool.Database.DBRepos;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility;
using MediatR;

namespace LiberPrimusAnalysisTool.Analyzers
{
    /// <summary>
    /// Indexes the liber primus pages into the database.
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
        /// Handler
        /// </summary>
        public class Handler : INotificationHandler<IndexPages.Command>
        {
            /// <summary>
            /// The liber page data
            /// </summary>
            private readonly ILiberPageData _liberPageData;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            /// <param name="liberPageData">The liber page data.</param>
            public Handler(ILiberPageData liberPageData)
            {
                _liberPageData = liberPageData;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var files = Directory.EnumerateFiles("./liber-primus__images--full").ToList();

                List<string> pageIds = new List<string>();
                for (int i = 0; i < 74; i++)
                {
                    pageIds.Add(i.ToString().PadLeft(2, '0'));
                }

                foreach(var pageId in pageIds)
                {
                    LoggingUtility.Log($"Processing {pageId}");
                    var file = files.Where(x => x.Contains(pageId)).First();
                    using (var imageFromFile = new MagickImage(file))
                    {
                        var page = new LiberPage
                        {
                            PageName = pageId,
                            PageSig = imageFromFile.Signature,
                            TotalColors = imageFromFile.TotalColors,
                            Height = imageFromFile.Height,
                            Width = imageFromFile.Width
                        };

                        _liberPageData.UpsertLiberPage(page);
                    }
                }
            }
        }
    }
}