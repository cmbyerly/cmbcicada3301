using LiberPrimusAnalysisTool.Application.Commands;
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
            var dontExit = true;

            while (dontExit)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red]THE ELASTIC STACK MUST BE RUNNING!!![/]");
                AnsiConsole.MarkupLine(string.Empty);
                AnsiConsole.MarkupLine("[green]Running Liber Primus Analysis Tool[/]");

                var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Please select analysis to run[/]:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more tests)[/]")
                    .AddChoices(new[] {
                        "0: Exit Program",
                        "1: Color Count Line (Kibana - Index: colorcounttext)",
                        "2: Color Counts in File (Kibana - Index: colorbreakdowntext)",
                        "3: Color Report (Kibana - Index: colorreport)",
                        "4: Reverse Bytes (output folder)",
                        "5: Index Pages (Kibana - Index: pageindex)",
                        "9999: All Tests",
                    }));

                var choice = selecttion.Split(":")[0];

                // Echo the fruit back to the terminal
                AnsiConsole.WriteLine($"Selected - {selecttion}");

                switch (choice.Trim())
                {
                    case "0":
                        dontExit = false;
                        break;

                    case "1":
                        _mediator.Publish(new ColorCountText.Command()).Wait();
                        break;

                    case "2":
                        _mediator.Publish(new ColorBreakDownText.Command()).Wait();
                        break;

                    case "3":
                        _mediator.Publish(new ColorReport.Command()).Wait();
                        break;

                    case "4":
                        _mediator.Publish(new ReverseBytes.Command()).Wait();
                        break;

                    case "5":
                        _mediator.Publish(new IndexPages.Command()).Wait();
                        break;

                    case "9999":
                        _mediator.Publish(new IndexPages.Command()).Wait();
                        _mediator.Publish(new ColorCountText.Command()).Wait();
                        _mediator.Publish(new ColorBreakDownText.Command()).Wait();
                        _mediator.Publish(new ColorReport.Command()).Wait();
                        break;

                    default:
                        AnsiConsole.Markup("[red]Not a valid choice.[/]");
                        break;
                }
            }
        }
    }
}