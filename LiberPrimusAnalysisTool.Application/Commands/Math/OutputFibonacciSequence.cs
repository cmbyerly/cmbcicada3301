using LiberPrimusAnalysisTool.Application.Queries.Math;
using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Math
{
    /// <summary>
    /// Output Fibonacci Sequence
    /// </summary>
    public class OutputFibonacciSequence
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.INotification" />
        public class Command : MediatR.INotification
        {
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : INotificationHandler<Command>
        {
            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="mediator">The mediator.</param>
            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            /// <summary>
            /// Handles the specified request.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var number = AnsiConsole.Ask<int>("What is the max number?");
                var fibonacciSequence = await _mediator.Send(new GetFibonacciSequence.Query() { MaxNumber = number });
                foreach (int i in fibonacciSequence)
                {
                    AnsiConsole.MarkupLine($"[green]{i}[/]");
                    await File.AppendAllTextAsync($"./output/math/fibonacci-{number}.txt", $"{i}" + Environment.NewLine);
                }
            }
        }
    }
}