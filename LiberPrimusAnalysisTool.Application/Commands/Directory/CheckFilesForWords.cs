using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace LiberPrimusAnalysisTool.Application.Commands.Directory
{
    /// <summary>
    /// Get Word From Ints
    /// </summary>
    public class CheckFilesForWords
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
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            public Handler(ICharacterRepo characterRepo)
            {
                _characterRepo = characterRepo;
            }

            /// <summary>
            /// Handles the specified request.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                List<string> scoreLines = new List<string>();
                var files = System.IO.Directory.GetFiles("./output/bytep/", "*.txt").OrderBy(x => x);

                foreach (var pfile in files)
                {
                    AnsiConsole.WriteLine($"Checking {pfile} for words...");

                    long score = 0;
                    using (var file = File.OpenText("words.txt"))
                    using (var checkfile = File.OpenText(pfile))
                    {
                        string line;
                        string checkline;
                        while ((checkline = checkfile.ReadLine()) != null)
                        {
                            while ((line = file.ReadLine()) != null)
                            {
                                if (checkline.ToUpper().Contains(line.ToUpper()))
                                {
                                    score++;
                                }
                            }
                        }

                        file.Close();
                        checkfile.Close();
                        file.Dispose();
                        checkfile.Dispose();
                    }   

                    if (score > 0)
                    {
                        AnsiConsole.WriteLine($"{pfile} - {score}");
                        scoreLines.Add($"{pfile} - {score}");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("No words found.");
                    }
                }

                File.WriteAllLines("output/word_score.txt", scoreLines);
            }
        }
    }
}