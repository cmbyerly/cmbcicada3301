using LiberPrimusAnalysisTool.Entity;
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
                var isGpStrict = AnsiConsole.Confirm("Use GP strict spellings?");
                var allFiles = System.IO.Directory.GetFiles("./output/bytep/").OrderBy(x => x);

                AnsiConsole.Status()
                    .AutoRefresh(true)
                    .Spinner(Spinner.Known.Circle)
                    .SpinnerStyle(Style.Parse("green bold"))
                    .Start("Processing files...", ctx =>
                    {
                        AnsiConsole.MarkupLine("Reading dictionary...");
                        ctx.Refresh();
                        using (var file = File.OpenText("words.txt"))
                        {
                            string line;
                            while ((line = file.ReadLine()) != null)
                            {
                                if (isGpStrict)
                                {
                                    englishDictionary.Add(line.ToUpper().Replace("QU", "KW").Replace("Q", "K").Replace("V", "U"));
                                }
                                else
                                {
                                    englishDictionary.Add(line.ToUpper());
                                }
                            }

                            file.Close();
                            file.Dispose();
                        }

                        AnsiConsole.MarkupLine("Dictionary read.");
                        ctx.Refresh();

                        englishDictionary = englishDictionary.OrderBy(x => x.Length).ToList();

                        for (int i = 0; i <= 74; i++)
                        {
                            AnsiConsole.MarkupLine($"Processing {i.ToString().PadLeft(2, '0')}...");
                            ctx.Refresh();
                            var files = allFiles.Where(x => x.Contains(i.ToString().PadLeft(2, '0'))).OrderBy(x => x).ToList();
                            var scorelines = files.AsParallel().Select(x => new ScoreLine(x, File.ReadAllLines(x), englishDictionary)).OrderBy(x => x.Score);
                            File.WriteAllLines($"output/word_score_{i.ToString().PadLeft(2, '0')}.txt", scorelines.Select(x => x.ToString()));
    
                            foreach (var score in scorelines)
                            {
                                if (score.Score > 0)
                                {
                                    File.AppendAllText($"output/word_readible_{i.ToString().PadLeft(2, '0')}.txt", score.FileName);
                                    File.AppendAllText($"output/word_readible_{i.ToString().PadLeft(2, '0')}.txt", Environment.NewLine);
                                    File.AppendAllLines($"output/word_readible_{i.ToString().PadLeft(2, '0')}.txt", score.ReadableLines);
                                    File.AppendAllText($"output/word_readible_{i.ToString().PadLeft(2, '0')}.txt", Environment.NewLine);
                                    File.AppendAllText($"output/word_readible_{i.ToString().PadLeft(2, '0')}.txt", Environment.NewLine);
                                }
                            }
                        }
                    });
            }
        }
    }
}