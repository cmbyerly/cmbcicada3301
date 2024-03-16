using LiberPrimusAnalysisTool.Entity;
using MediatR;

namespace LiberPrimusAnalysisTool.Application.Queries.Math
{
    /// <summary>
    /// Get Totient Sequence
    /// </summary>
    public class GetTotientSequence
    {
        /// <summary>
        /// Query
        /// </summary>
        public class Query : IRequest<Totient>
        {
            /// <summary>
            /// Gets or sets the number.
            /// </summary>
            /// <value>
            /// The number.
            /// </value>
            public int Number { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : IRequestHandler<Query, Totient>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<Totient> Handle(Query request, CancellationToken cancellationToken)
            {
                var totient = new Totient();
                totient.Number = request.Number;
                var n = request.Number;

                for (int i = 1; i <= n; i++)
                {
                    if (GCD(i, n) == 1)
                    {
                        totient.Sequence.Add(i);
                    }
                }

                totient.Phi = totient.Sequence.Count;

                return totient;
            }

            /// <summary>
            /// GCDs the specified a.
            /// </summary>
            /// <param name="a">a.</param>
            /// <param name="b">The b.</param>
            /// <returns></returns>
            private int GCD(int a, int b)
            {
                while (b != 0)
                {
                    var t = b;
                    b = a % b;
                    a = t;
                }

                return a;
            }
        }
    }
}