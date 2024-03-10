using LiberPrimusAnalysisTool.Database.DBInterfaces;
using MediatR;

namespace LiberPrimusAnalysisTool.Analyzers
{
    /// <summary>
    /// PixelReport
    /// </summary>
    public class PixelReport
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
        /// <seealso cref="MediatR.IRequestHandler&lt;LiberPrimusAnalysisTool.Analyzers.PixelReport.Command&gt;" />
        public class Handler : INotificationHandler<PixelReport.Command>
        {
            /// <summary>
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            public Handler(ICharacterRepo characterRepo)
            {
                _characterRepo = characterRepo;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
            }
        }
    }
}