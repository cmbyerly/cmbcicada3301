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
                Console.Clear();
                AnsiConsole.Write(new FigletText("Credits").Centered().Color(Color.Green));
                AnsiConsole.WriteLine("Words - https://github.com/dwyl/english-words");
                AnsiConsole.WriteLine("Inspiration for sequence code - https://github.com/TheAlgorithms/C-Sharp");
                AnsiConsole.WriteLine("File detections - https://github.com/ghost1face/FileTypeInterrogator");
                AnsiConsole.Confirm("Press [green]Enter[/] to continue...");
            }
        }
    }
}