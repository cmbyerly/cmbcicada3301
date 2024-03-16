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
        public class Command : IRequest<IEnumerable<int>>
        {
            public int MaxNumber { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Command, IEnumerable<int>>
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
            public async Task<IEnumerable<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                List<int> result = new List<int>();

                int a = 0;
                int b = 1;
                int c = 0;

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