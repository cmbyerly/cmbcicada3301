using MediatR;

namespace LiberPrimusAnalysisTool.Application.Queries
{
    /// <summary>
    /// Indexes the liber primus pages into the database.
    /// </summary>
    public class GetIsPrime
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : MediatR.IRequest<bool>
        {
            public int Number { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Command, bool>
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
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Number <= 1) return false;
                if (request.Number == 2) return true;
                if (request.Number % 2 == 0) return false;

                var boundary = (int)System.Math.Floor(System.Math.Sqrt(request.Number));

                for (int i = 3; i <= boundary; i += 2)
                {
                    if (request.Number % i == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}