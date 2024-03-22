using ImageMagick;
using LiberPrimusAnalysisTool.Entity;
using MediatR;
using Spectre.Console;
using System.Drawing;

namespace LiberPrimusAnalysisTool.Application.Queries.Page
{
    /// <summary>
    /// Indexes the liber primus pages into the database.
    /// </summary>
    public class GetPages
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="IRequest" />
        public class Query : IRequest<IEnumerable<LiberPage>>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Query"/> class.
            /// </summary>
            /// <param name="includeImageData">if set to <c>true</c> [include image data].</param>
            public Query(bool includeImageData)
            {
                IncludeImageData = includeImageData;
            }

            /// <summary>
            /// Gets or sets a value indicating whether [include image data].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [include image data]; otherwise, <c>false</c>.
            /// </value>
            public bool IncludeImageData { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Query, IEnumerable<LiberPage>>
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
            public async Task<IEnumerable<LiberPage>> Handle(Query request, CancellationToken cancellationToken)
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
                    AnsiConsole.WriteLine($"Page Listing: {pageId}");
                    var file = files.Where(x => x.Contains(pageId)).First();

                    if (request.IncludeImageData)
                    {
                        using (var imageFromFile = new MagickImage(file))
                        using (var pixels = imageFromFile.GetPixels())
                        {
                            var page = new LiberPage
                            {
                                FileName = file,
                                PageName = pageId,
                                PageSig = imageFromFile.Signature,
                                TotalColors = imageFromFile.TotalColors,
                                Height = imageFromFile.Height,
                                Width = imageFromFile.Width,
                                PixelCount = pixels.Count()
                            };

                            page.Pixels = pixels.Select(x => new Entity.Pixel(
                                x.X,
                                x.Y,
                                ColorTranslator.FromHtml(x.ToColor().ToHexString().ToUpper()).R,
                                ColorTranslator.FromHtml(x.ToColor().ToHexString().ToUpper()).G,
                                ColorTranslator.FromHtml(x.ToColor().ToHexString().ToUpper()).B,
                                x.ToColor().ToHexString(),
                                page.PageName)).ToList();

                            pages.Add(page);
                            pixels.Dispose();
                        }

                        GC.Collect();
                    }
                    else
                    {
                        var page = new LiberPage
                        {
                            FileName = file,
                            PageName = pageId
                        };

                        pages.Add(page);
                    }
                }

                return pages;
            }
        }
    }
}