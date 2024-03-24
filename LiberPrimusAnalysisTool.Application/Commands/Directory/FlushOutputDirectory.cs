using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Directory
{
    /// <summary>
    /// Flush Output Directory
    /// </summary>
    public class FlushOutputDirectory
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
                AnsiConsole.Write(new FigletText("Flush output directories").Centered().Color(Color.Green));
                if (System.IO.Directory.Exists("output"))
                {
                    System.IO.Directory.EnumerateFiles("output").ToList().ForEach(f => File.Delete(f));
                }

                if (System.IO.Directory.Exists("./output/imagep"))
                {
                    System.IO.Directory.EnumerateFiles("./output/imagep").ToList().ForEach(f => File.Delete(f));
                }

                if (System.IO.Directory.Exists("./output/bytep"))
                {
                    System.IO.Directory.EnumerateFiles("./output/bytep").ToList().ForEach(f => File.Delete(f));
                }

                if (System.IO.Directory.Exists("./output/math"))
                {
                    System.IO.Directory.EnumerateFiles("./output/math").ToList().ForEach(f => File.Delete(f));
                }
            }
        }
    }
}