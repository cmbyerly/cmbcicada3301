using MediatR;

namespace LiberPrimusAnalysisTool.Application.Queries.Math
{
    /// <summary>
    /// Get Fibonacci Sequence
    /// </summary>
    public class GetFibonacciSequence
    {
        /// <summary>
        /// Command
        /// </summary>
        public class Query : IRequest<IEnumerable<long>>
        {
            public long MaxNumber { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Query, IEnumerable<long>>
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
            public async Task<IEnumerable<long>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<long> result = new List<long>();

                long a = 0;
                long b = 1;
                long c = 0;

                while (c <= request.MaxNumber)
                {
                    c = a + b;
                    a = b;
                    b = c;

                    if (c <= request.MaxNumber)
                    {
                        result.Add(c);
                    }
                }

                return result;
            }
        }
    }
}