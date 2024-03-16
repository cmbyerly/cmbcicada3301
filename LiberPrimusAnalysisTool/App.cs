using LiberPrimusAnalysisTool.Application.Commands.Directory;
using LiberPrimusAnalysisTool.Application.Commands.Image;
using LiberPrimusAnalysisTool.Application.Commands.Math;
using MediatR;
using Spectre.Console;

namespace LiberPrimusAnalysisTool
{
    /// <summary>
    /// App
    /// </summary>
    public class App
    {
        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public App(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Runs the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Run(string[] args)
        {
            _mediator.Publish(new CheckForOutputDirectory.Command()).Wait();

            var dontExit = true;

            while (dontExit)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Running Liber Primus Analysis Tool[/]");

                var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Please select analysis to run[/]:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more tests)[/]")
                    .AddChoices(new[] {
                        "1: Flush output directory",
                        "2: Reverse bytes",
                        "3: RGB -> Text",
                        "4: Check if number is prime",
                        "5: Output prime sequence",
                        "6: Output fibonacci sequence",
                        "7: Calculate Totient",
                        "8: Output Page Information",
                        "99: Exit Program",
                    }));

                var choice = selecttion.Split(":")[0];

                // Echo the fruit back to the terminal
                AnsiConsole.WriteLine($"Selected - {selecttion}");

                switch (choice.Trim())
                {
                    case "1":
                        _mediator.Publish(new FlushOutputDirectory.Command()).Wait();
                        break;

                    case "2":
                        _mediator.Publish(new ReverseBytes.Command()).Wait();
                        break;

                    case "3":
                        _mediator.Publish(new RgbIndex.Command()).Wait();
                        break;

                    case "4":
                        _mediator.Publish(new CheckIfNumberIsPrime.Command()).Wait();
                        break;

                    case "5":
                        _mediator.Publish(new OutputPrimeSequence.Command()).Wait();
                        break;

                    case "6":
                        _mediator.Publish(new OutputFibonacciSequence.Command()).Wait();
                        break;

                    case "7":
                        _mediator.Publish(new CalculateTotient.Command()).Wait();
                        break;

                    case "8":
                        _mediator.Publish(new OutputPageInformation.Command()).Wait();
                        break;

                    case "99":
                        dontExit = false;
                        break;

                    default:
                        AnsiConsole.Markup("[red]Not a valid choice.[/]");
                        break;
                }
            }
        }
    }
}