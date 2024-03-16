using MediatR;

namespace LiberPrimusAnalysisTool.Application.Queries
{
    /// <summary>
    /// Get the prime sequence
    /// </summary>
    public class GetPrimeSequence
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="IRequest" />
        public class Query : IRequest<IEnumerable<int>>
        {
            public int Number { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Query, IEnumerable<int>>
        {
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<IEnumerable<int>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<int> result = new List<int>();

                for (int i = 0; i <= request.Number; i++)
                {
                    var isPrime = await _mediator.Send(new GetIsPrime.Query() { Number = i });

                    if (isPrime)
                    {
                        result.Add(i);
                    }
                }

                return result;
            }
        }
    }
}