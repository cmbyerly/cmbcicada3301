using LiberPrimusAnalysisTool.Analyzers;
using LiberPrimusAnalysisTool.Utility;
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
            FileUtilities.RemovePreviousRuns();

            var dontExit = true;

            while (dontExit)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red]THIS TOOL REMOVES OUTPUT BETWEEN EXECUTIONS!!![/]");
                AnsiConsole.MarkupLine("[red]THE DATABASE MUST BE RUNNING!!![/]");
                AnsiConsole.MarkupLine(string.Empty);
                AnsiConsole.MarkupLine("[green]Running Liber Primus Analysis Tool[/]");
                AnsiConsole.MarkupLine("[green]Enter the test number you want to perform.[/]");

                var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Please select test to run[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more tests)[/]")
                    .AddChoices(new[] {
                        "0: Exit Program",
                        "1: Index Liber Primus Pages",
                        "96: Round 1 Test (output folder)",
                        "97: Round 2 Test (output folder)",
                        "98: Color Report (output folder)",
                        "99: Reverse Bytes (output folder)",
                        "9999: All Tests",
                    }));

                var choice = selecttion.Split(":")[0];

                // Echo the fruit back to the terminal
                AnsiConsole.WriteLine($"Selected test: {selecttion}");

                switch (choice.Trim())
                {
                    case "0":
                        dontExit = false;
                        break;

                    case "1":
                        _mediator.Publish(new IndexPages.Command()).Wait();
                        break;

                    case "96":
                        _mediator.Publish(new ColorCountText.Command()).Wait();
                        break;

                    case "97":
                        _mediator.Publish(new ColorBreakDownText.Command()).Wait();
                        break;

                    case "98":
                        _mediator.Publish(new ColorReport.Command()).Wait();
                        break;

                    case "99":
                        _mediator.Publish(new ReverseBytes.Command()).Wait();
                        break;

                    case "9999":
                        _mediator.Publish(new ColorCountText.Command()).Wait();
                        _mediator.Publish(new ColorBreakDownText.Command()).Wait();
                        _mediator.Publish(new ColorBreakDownText.Command()).Wait();
                        break;

                    default:
                        AnsiConsole.Markup("[red]Not a valid choice.[/]");
                        break;
                }
            }
        }
    }
}