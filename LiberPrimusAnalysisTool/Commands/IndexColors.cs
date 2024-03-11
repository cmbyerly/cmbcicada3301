using ImageMagick;
using LiberPrimusAnalysisTool.Database.DBRepos;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility;
using MediatR;

namespace LiberPrimusAnalysisTool.Commands
{
    public class IndexColors
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
        public class Handler : INotificationHandler<IndexColors.Command>
        {
            /// <summary>
            /// The liber page data
            /// </summary>
            private readonly ILiberPageData _liberPageData;

            /// <summary>
            /// The liber color data
            /// </summary>
            private readonly ILiberColorData _liberColorData;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="liberPageData">The liber page data.</param>
            /// <param name="liberColorData">The liber color data.</param>
            public Handler(ILiberPageData liberPageData, ILiberColorData liberColorData)
            {
                _liberPageData = liberPageData;
                _liberColorData = liberColorData;
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

                foreach (var pageId in pageIds)
                {
                    long counter = 0;
                    long pixelCount = 0;
                    LoggingUtility.Log($"Processing {pageId}");
                    var pageData = _liberPageData.GetLiberPage(pageId);
                    var file = files.Where(x => x.Contains(pageId)).First();
                    using (var imageFromFile = new MagickImage(file))
                    {
                        var pixels = imageFromFile.GetPixels();
                        pixelCount = pixels.Count();
                        foreach (var color in pixels)
                        {
                            var liberColor = new LiberColor
                            {
                                LiberColorHex = color.ToColor().ToHexString()
                            };

                            LoggingUtility.Log($"Processing: {pageId} - Upserting: {liberColor} - Position: {counter}/{pixelCount}");

                            _liberColorData.UpsertLiberColor(liberColor);
                            counter++;
                        }
                    }
                }
            }
        }
    }
}
