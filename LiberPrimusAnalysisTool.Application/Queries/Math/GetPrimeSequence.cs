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
        public class Query : IRequest<IEnumerable<long>>
        {
            public long Number { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Query, IEnumerable<long>>
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
            public async Task<IEnumerable<long>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<long> result = new List<long>();

                for (long i = 0; i <= request.Number; i++)
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