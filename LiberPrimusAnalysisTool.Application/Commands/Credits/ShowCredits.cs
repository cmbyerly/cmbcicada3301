using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Directory
{
    /// <summary>
    /// Flush Output Directory
    /// </summary>
    public class ShowCredits
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
            /// Handles the specified request.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                AnsiConsole.WriteLine("Words - https://github.com/dwyl/english-words");
                AnsiConsole.WriteLine("Inspiration for sequence code - https://github.com/TheAlgorithms/C-Sharp");
                AnsiConsole.Confirm("Press [green]Enter[/] to continue...");
            }
        }
    }
}