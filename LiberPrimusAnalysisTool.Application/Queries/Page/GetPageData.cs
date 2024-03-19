using ImageMagick;
using LiberPrimusAnalysisTool.Entity;
using MediatR;
using Spectre.Console;
using System.Drawing;

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
        public class Query : MediatR.IRequest<LiberPage>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Query"/> class.
            /// </summary>
            /// <param name="pageId">The page identifier.</param>
            /// <param name="includePixels">if set to <c>true</c> [include pixels].</param>
            public Query(string pageId, bool includePixels)
            {
                PageId = pageId;
                IncludePixels = includePixels;
            }

            /// <summary>
            /// Gets or sets the page identifier.
            /// </summary>
            /// <value>
            /// The page identifier.
            /// </value>
            public string PageId { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [include pixels].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [include pixels]; otherwise, <c>false</c>.
            /// </value>
            public bool IncludePixels { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Query, LiberPage>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            public Handler()
            {
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<LiberPage> Handle(Query request, CancellationToken cancellationToken)
            {
                LiberPage page;
                var files = Directory.EnumerateFiles("./liber-primus__images--full").ToList();

                AnsiConsole.WriteLine($"Getting page info for {request.PageId}");
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
                        Width = imageFromFile.Width,
                        PixelCount = imageFromFile.GetPixels().Count()
                    };

                    if (request.IncludePixels)
                    {
                        page.Pixels = imageFromFile.GetPixels().Select(x => new Entity.Pixel(
                            x.X, 
                            x.Y,
                            ColorTranslator.FromHtml(x.ToColor().ToHexString().ToUpper()).R,
                            ColorTranslator.FromHtml(x.ToColor().ToHexString().ToUpper()).G,
                            ColorTranslator.FromHtml(x.ToColor().ToHexString().ToUpper()).B,
                            x.ToColor().ToHexString(),
                            page.PageName)).ToList();
                    }
                }

                return page;
            }
        }
    }
}