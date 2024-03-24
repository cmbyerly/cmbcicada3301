﻿using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;
using System.Text;

namespace LiberPrimusAnalysisTool.Application.Commands.Directory
{
    /// <summary>
    /// Get Word From Ints
    /// </summary>
    public class GetWordFromInts
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
                AnsiConsole.WriteLine("Getting words scoring a certain value.");
                var values = AnsiConsole.Ask<string>("Enter comma seperated values (e.g. 1,231,82):");
                StringBuilder actualValues = new StringBuilder();
                for (int i = 0; i < values.Length; i++)
                {
                    if (char.IsDigit(values[i]) || values[i] == ',')
                    {
                        actualValues.Append(values[i]);
                    }
                }

                foreach (var valueString in actualValues.ToString().Split(','))
                {
                    List<string> words = new List<string>();
                    int value = int.Parse(valueString);
                    using (var file = File.OpenText("words.txt"))
                    {
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.ToUpper().Contains("Q"))
                            {
                                continue;
                            }

                            int lineValue = ScoreDictionaryValue(line.ToUpper(), 0);
                            if (lineValue == value)
                            {
                                AnsiConsole.WriteLine($"{line} has value: {value}");
                                words.Add(line);
                            }
                        }

                        file.Close();
                        file.Dispose();
                    }

                    if (words.Count > 0)
                    {
                        AnsiConsole.WriteLine($"Saving file ./output/words_{value}.txt");

                        using (var file = File.CreateText($"./output/words_{value}.txt"))
                        {
                            foreach (var word in words)
                            {
                                file.WriteLine(word);
                            }

                            file.Close();
                            file.Dispose();
                        }
                    }
                    else
                    {
                        AnsiConsole.WriteLine("No words found.");
                    }
                }
            }

            /// <summary>
            /// Scores the dictionary value.
            /// </summary>
            /// <param name="word">The word.</param>
            /// <param name="wordValue">The word value.</param>
            /// <returns></returns>
            private int ScoreDictionaryValue(string word, int wordValue)
            {
                string[] cicadaletters = _characterRepo.GetGematriaStrings();

                int currentWordValue = wordValue;

                foreach (var letter in cicadaletters)
                {
                    if (word.Contains(letter))
                    {
                        currentWordValue += _characterRepo.GetValueFromString(letter);
                        word = word.Replace(letter, string.Empty);
                        break;
                    }
                }

                if (word.Length == 0)
                {
                    return currentWordValue;
                }
                else
                {
                    return ScoreDictionaryValue(word, currentWordValue);
                }
            }
        }
    }
}