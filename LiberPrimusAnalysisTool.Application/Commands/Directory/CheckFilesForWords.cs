using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;
using System.Text;

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
                List<string> englishDictionary = new List<string>();
                Dictionary<string, int> characterMap = new Dictionary<string, int>();
                List<Tuple<string, string>> wordMap = new List<Tuple<string, string>>();

                var files = System.IO.Directory.GetFiles("./output/bytep/").OrderBy(x => x);

                using (var file = File.OpenText("words.txt"))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        englishDictionary.Add(line);
                    }

                    file.Close();
                    file.Dispose();
                }

                ParallelOptions parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 8
                };

                Parallel.ForEach(files, parallelOptions, pfile =>
                {
                    AnsiConsole.WriteLine($"Checking {pfile} for words...");
                    StringBuilder words = new StringBuilder();

                    int score = 0;
                    using (var checkfile = File.OpenText(pfile))
                    {
                        string checkline;
                        while ((checkline = checkfile.ReadLine()) != null)
                        {
                            foreach (var line in englishDictionary)
                            {
                                if (checkline.ToUpper().Contains(line.ToUpper()))
                                {
                                    score++;
                                    words.Append($"{line} ");
                                }
                            }

                            if (score > 0)
                            {
                                words.Append(Environment.NewLine);
                            }
                        }

                        checkfile.Close();
                        checkfile.Dispose();
                    }

                    if (score > 0)
                    {
                        AnsiConsole.WriteLine($"{pfile} - {score}");
                        wordMap.Add(new Tuple<string, string>(pfile, words.ToString()));
                        characterMap.Add(pfile, score);
                    }
                    else
                    {
                        AnsiConsole.WriteLine("No words found.");
                    }
                });

                var scoreLines = characterMap.OrderByDescending(x => x.Value).Select(x => $"{x.Key} - {x.Value}");

                File.WriteAllLines("output/word_score.txt", scoreLines);

                var sortedScore = characterMap.OrderByDescending(x => x.Value).ToList();
                foreach (var score in sortedScore)
                {
                    var value = wordMap.FirstOrDefault(x => x.Item1 == score.Key);
                    File.AppendAllText("output/word_readible.txt", $"{value.Item1} - {value.Item2}");
                    File.AppendAllText("output/word_readible.txt", string.Empty);
                }
            }
        }
    }
}