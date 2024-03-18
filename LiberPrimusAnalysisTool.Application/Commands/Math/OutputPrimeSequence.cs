﻿using LiberPrimusAnalysisTool.Application.Queries;
using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Math
{
    /// <summary>
    /// Output Prime Sequence
    /// </summary>
    public class OutputPrimeSequence
    {
        /// <summary>
        ///
        /// </summary>
        /// <seealso cref="MediatR.INotification" />
        public class Command : INotification
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
                var number = AnsiConsole.Ask<int>("What is the number?");
                var primeSequence = await _mediator.Send(new GetPrimeSequence.Query() { Number = number });
                foreach (int i in primeSequence)
                {
                    AnsiConsole.MarkupLine($"[green]{i} is prime[/]");
                    await File.AppendAllTextAsync("./output/prime.txt", $"{i}" + Environment.NewLine);
                }
            }
        }
    }
}