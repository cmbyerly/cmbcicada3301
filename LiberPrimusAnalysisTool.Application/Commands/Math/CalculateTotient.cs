using LiberPrimusAnalysisTool.Application.Queries.Math;
using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Directory
{
    /// <summary>
    /// Calculate Totient
    /// </summary>
    public class CalculateTotient
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
                bool returnToMenu = false;

                while (!returnToMenu)
                {
                    var number = AnsiConsole.Ask<int>("What is the number?");

                    AnsiConsole.MarkupLine($"Calculating Totient for {number}");
                    var totient = await _mediator.Send(new GetTotientSequence.Query() { Number = number });

                    AnsiConsole.MarkupLine($"[green]Phi({number}) = {totient.Phi}[/]");
                    AnsiConsole.MarkupLine($"[green]Sequence: {string.Join($"{Environment.NewLine}", totient.Sequence)}[/]");

                    await File.AppendAllTextAsync("./output/totient.txt", $"Number: {totient.Number}" + Environment.NewLine);
                    await File.AppendAllTextAsync("./output/totient.txt", $"Phi: {totient.Phi}" + Environment.NewLine);
                    await File.AppendAllTextAsync("./output/totient.txt", "Sequence: " + Environment.NewLine);
                    await File.AppendAllTextAsync("./output/totient.txt", string.Empty + Environment.NewLine);
                    foreach (var item in totient.Sequence)
                    {
                        await File.AppendAllTextAsync("./output/totient.txt", $"{item}" + Environment.NewLine);
                    }

                    returnToMenu = AnsiConsole.Confirm("Return to main menu?");
                }
            }
        }
    }
}