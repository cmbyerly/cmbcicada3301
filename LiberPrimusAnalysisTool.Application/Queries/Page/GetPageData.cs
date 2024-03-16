using ImageMagick;
using LiberPrimusAnalysisTool.Entity;
using MediatR;
using Spectre.Console;

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
            public async Task<LiberPage> Handle(Command request, CancellationToken cancellationToken)
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
                }

                return page;
            }
        }
    }
}