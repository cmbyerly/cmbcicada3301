using ImageMagick;
using LiberPrimusAnalysisTool.Application.Queries;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Character;
using LiberPrimusAnalysisTool.Utility.Logging;
using MediatR;
using Microsoft.Extensions.Configuration;
using Nest;
using Spectre.Console;
using System.Drawing;

namespace LiberPrimusAnalysisTool.Application.Commands
{
    /// <summary>
    /// ColorReport
    /// </summary>
    public class RgbIndex
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="MediatR.IRequest" />
        public class Command : INotification
        {
        }

        /// <summary>
        ///Handler
        /// </summary>
        /// <seealso cref="MediatR.IRequestHandler&lt;LiberPrimusAnalysisTool.Analyzers.ColorReport.Command&gt;" />
        public class Handler : INotificationHandler<RgbIndex.Command>
        {
            /// <summary>
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// The logging utility
            /// </summary>
            private readonly ILoggingUtility _loggingUtility;

            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            /// <param name="loggingUtility">The logging utility.</param>
            /// <param name="mediator">The mediator.</param>
            public Handler(ICharacterRepo characterRepo, ILoggingUtility loggingUtility, IMediator mediator)
            {
                _characterRepo = characterRepo;
                _loggingUtility = loggingUtility;
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var selecttion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Include control characters?[/]?")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Yes",
                        "No"
                    }));

                var includeControlCharacters = selecttion == "Yes";

                var files = await _mediator.Send(new GetPages.Command(false));

                foreach (var file in files)
                {
                    await _loggingUtility.Log($"Processing {file}");
                    var rgbIndex = new RgbCharacters(file.PageName);

                    using (var imageFromFile = new MagickImage(file.FileName))
                    {
                        var pixels = imageFromFile.GetPixels();

                        await _loggingUtility.Log($"Document: {file} - RGB Breakdown");
                        foreach (Pixel pixel in pixels)
                        {
                            System.Drawing.Color color = ColorTranslator.FromHtml(pixel.ToColor().ToHexString().ToUpper());
                            rgbIndex.AddR(_characterRepo.GetASCIICharFromDec(color.R, includeControlCharacters));
                            rgbIndex.AddG(_characterRepo.GetASCIICharFromDec(color.G, includeControlCharacters));
                            rgbIndex.AddB(_characterRepo.GetASCIICharFromDec(color.B, includeControlCharacters));
                        }

                        AnsiConsole.WriteLine($"Red Text: {rgbIndex.R}");
                        AnsiConsole.WriteLine($"Green Text: {rgbIndex.G}");
                        AnsiConsole.WriteLine($"Blue Text: {rgbIndex.B}");

                        await _loggingUtility.Log($"Writing: ./output/RgbIndex_{file.PageName}.txt");
                        File.AppendAllText($"./output/RgbIndex_{file.PageName}.txt", "Red Text:" + rgbIndex.R + Environment.NewLine + Environment.NewLine);
                        File.AppendAllText($"./output/RgbIndex_{file.PageName}.txt", "Green Text: " + rgbIndex.G + Environment.NewLine + Environment.NewLine);
                        File.AppendAllText($"./output/RgbIndex_{file.PageName}.txt", "Blue Text: " + rgbIndex.B + Environment.NewLine + Environment.NewLine);
                    }
                };
            }
        }
    }
}