using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;
using System.Text;

namespace LiberPrimusAnalysisTool.Application.Commands.Image
{
    /// <summary>
    /// ReverseBytes - Reverses the bytes of an image and compares the bytes
    /// </summary>
    public class ReverseBytes
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="IRequest" />
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
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            public Handler(ICharacterRepo characterRepo)
            {
                _characterRepo = characterRepo;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                List<string> choices = new List<string>();
                for (int i = 0; i < 74; i++)
                {
                    choices.Add(i.ToString().PadLeft(2, '0'));
                }

                var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Please select which images you want to reverse bytes[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more images)[/]")
                    .AddChoices(choices.ToArray()));

                var file = System.IO.Directory.EnumerateFiles("./liber-primus__images--full").ToList().Where(x => x.Contains(selecttion)).First();

                AnsiConsole.WriteLine($"Processing {file}");

                var byteArray = File.ReadAllBytes(file);
                var reversedByteArray = byteArray.Reverse().ToArray();

                StringBuilder reverseBuilder = new StringBuilder();

                AnsiConsole.WriteLine($"Reversed byte array for {file}");

                File.WriteAllBytes($"./output/{selecttion}-reversed.jpg", reversedByteArray);

                AnsiConsole.WriteLine($"Saved {selecttion}-reversed.jpg");

                AnsiConsole.WriteLine("Comparing Bytes");

                for (int i = 0; i < byteArray.Length; i++)
                {
                    if (byteArray[i] != reversedByteArray[i])
                    {
                        var reversedBin = Convert.ToString(Convert.ToInt32(reversedByteArray[i].ToString()), 2).PadLeft(7, '0');
                        var character = _characterRepo.GetASCIICharFromBin(reversedBin);
                        reverseBuilder.Append(character);
                        AnsiConsole.WriteLine($"Byte {i} is different - rev value is {reversedByteArray[i]}");
                        AnsiConsole.MarkupLine($"[lime]ASCII Character is {character}.[/]");
                    }
                }

                File.AppendAllLines($"./output/{selecttion}-reversed.txt", new string[] { reverseBuilder.ToString() });

                AnsiConsole.MarkupLine("[yellow]CHECK OUTPUT DIRECTORY.[/]");
                AnsiConsole.MarkupLine("[yellow]Press any key to continue.[/]");
                Console.ReadLine();
            }
        }
    }
}