using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Application.Commands.Directory
{
    /// <summary>
    /// Flush Output Directory
    /// </summary>
    public class DetectBinFiles
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
                AnsiConsole.Write(new FigletText("Detect Bin Files").Centered().Color(Color.Green));
                var files = System.IO.Directory.GetFiles("./output/bytep/", "*.bin");
                DetectBinaryFiles(files);

                files = System.IO.Directory.GetFiles("./output/imagep/", "*.bin");
                DetectBinaryFiles(files);
            }

            /// <summary>
            /// Detects the binary files.
            /// </summary>
            /// <param name="files">The files.</param>
            private static void DetectBinaryFiles(string[] files)
            {
                List<string> lines = new List<string>();

                if (files.Length == 0)
                {
                    AnsiConsole.WriteLine("No bin files found.");
                }
                else
                {
                    AnsiConsole.WriteLine("Bin files found:");
                    foreach (var file in files)
                    {
                        AnsiConsole.WriteLine(file);
                        FileTypeInterrogator.IFileTypeInterrogator interrogator = new FileTypeInterrogator.FileTypeInterrogator();

                        byte[] fileBytes = File.ReadAllBytes(file);

                        FileTypeInterrogator.FileTypeInfo fileTypeInfo = interrogator.DetectType(fileBytes);

                        if (fileTypeInfo == null)
                        {
                            AnsiConsole.WriteLine("Could not detect file type.");
                            continue;
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Name = " + fileTypeInfo.Name);
                            AnsiConsole.WriteLine("Extension = " + fileTypeInfo.FileType);
                            AnsiConsole.WriteLine("Mime Type = " + fileTypeInfo.MimeType);

                            lines.Add(file);
                            lines.Add("Name = " + fileTypeInfo.Name);
                            lines.Add("Extension = " + fileTypeInfo.FileType);
                            lines.Add("Mime Type = " + fileTypeInfo.MimeType);
                            lines.Add("");
                        }
                    }

                    File.AppendAllLines("./output/detect_bin_files.txt", lines);
                }
            }
        }
    }
}