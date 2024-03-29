﻿using LiberPrimusAnalysisTool.Application.Commands.Directory;
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
                AnsiConsole.Write(new FigletText("Liber Primus Analysis Tool").Centered().Color(Color.Green));

                var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Please select analysis to run[/]:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more tests)[/]")
                    .AddChoices(new[] {
                        "0: Initialize Database (DO THIS FIRST)",
                        "1: Flush output directory",
                        "2: Reverse bytes",
                        "3: RGB -> Text",
                        "4: Check if number is prime",
                        "5: Output prime sequence",
                        "6: Output fibonacci sequence",
                        "7: Calculate Totient",
                        "8: Winnow Page(s) (Bytes)",
                        "9: Winnow Page(s) (Non-GCT Pixel)",
                        "10: Isolate Color(s)",
                        "11: Get Words From Ints",
                        "12: Detect Bin Files",
                        "13: Isolate Color(s) (Var 2)",
                        "14: Detect Words in Files",
                        "98: Show Credits",
                        "99: Exit Program",
                    }));

                var choice = selecttion.Split(":")[0];

                // Echo the selection back to the terminal
                AnsiConsole.WriteLine($"Selected - {selecttion}");

                switch (choice.Trim())
                {
                    case "0":
                        _mediator.Publish(new OutputPageInformation.Command()).Wait();
                        break;

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
                        _mediator.Publish(new ByteWinnowPages.Command()).Wait();
                        break;

                    case "9":
                        _mediator.Publish(new PixelWinnowPages.Command()).Wait();
                        break;

                    case "10":
                        _mediator.Publish(new ColorIsolation.Command()).Wait();
                        break;

                    case "11":
                        _mediator.Publish(new GetWordFromInts.Command()).Wait();
                        break;

                    case "12":
                        _mediator.Publish(new DetectBinFiles.Command()).Wait();
                        break;

                    case "13":
                        _mediator.Publish(new ColorIsolationVar2.Command()).Wait();
                        break;

                    case "14":
                        _mediator.Publish(new CheckFilesForWords.Command()).Wait();
                        break;

                    case "98":
                        _mediator.Publish(new ShowCredits.Command()).Wait();
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