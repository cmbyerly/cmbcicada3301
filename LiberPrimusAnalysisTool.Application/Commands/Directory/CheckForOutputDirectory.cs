using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Directory
{
    /// <summary>
    /// Check For Output Directory
    /// </summary>
    public class CheckForOutputDirectory
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
                AnsiConsole.WriteLine("Checking for output directory");
                if (!System.IO.Directory.Exists("output"))
                {
                    System.IO.Directory.CreateDirectory("output");
                }

                if (!System.IO.Directory.Exists("./output/imagep"))
                {
                    System.IO.Directory.CreateDirectory("./output/imagep");
                }

                if (!System.IO.Directory.Exists("./output/bytep"))
                {
                    System.IO.Directory.CreateDirectory("./output/bytep");
                }

                if (!System.IO.Directory.Exists("./output/math"))
                {
                    System.IO.Directory.CreateDirectory("./output/math");
                }
            }
        }
    }
}