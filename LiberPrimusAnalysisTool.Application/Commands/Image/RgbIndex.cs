﻿using ImageMagick;
using LiberPrimusAnalysisTool.Application.Queries.Page;
using LiberPrimusAnalysisTool.Entity;
using LiberPrimusAnalysisTool.Utility.Character;
using MediatR;
using Spectre.Console;
using System.Drawing;

namespace LiberPrimusAnalysisTool.Application.Commands.Image
{
    /// <summary>
    /// ColorReport
    /// </summary>
    public class RgbIndex
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="IRequest" />
        public class Command : INotification
        {
        }

        /// <summary>
        ///Handler
        /// </summary>
        /// <seealso cref="IRequestHandler&lt;LiberPrimusAnalysisTool.Analyzers.ColorReport.Command&gt;" />
        public class Handler : INotificationHandler<Command>
        {
            /// <summary>
            /// The character repo
            /// </summary>
            private readonly ICharacterRepo _characterRepo;

            /// <summary>
            /// The mediator
            /// </summary>
            private readonly IMediator _mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            /// <param name="characterRepo">The character repo.</param>
            /// <param name="mediator">The mediator.</param>
            public Handler(ICharacterRepo characterRepo, IMediator mediator)
            {
                _characterRepo = characterRepo;
                _mediator = mediator;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText("RGB Index").Centered().Color(Spectre.Console.Color.Green));

                var invertPixels = AnsiConsole.Confirm("Invert Pixels?");

                var includeControlCharacters = AnsiConsole.Confirm("Include control characters?");

                var files = await _mediator.Send(new GetPages.Query(false));

                Parallel.ForEach(files, file =>
                {
                    AnsiConsole.WriteLine($"Processing {file}");
                    var rgbIndex = new RgbCharacters(file.PageName);

                    using (var imageFromFile = new MagickImage(file.FileName))
                    {
                        var pixels = imageFromFile.GetPixels();

                        AnsiConsole.WriteLine($"Document: {file} - RGB Breakdown");
                        foreach (ImageMagick.Pixel pixel in pixels)
                        {
                            System.Drawing.Color color = ColorTranslator.FromHtml(pixel.ToColor().ToHexString().ToUpper());
                            rgbIndex.AddR(_characterRepo.GetASCIICharFromDec(color.R, includeControlCharacters));
                            rgbIndex.AddG(_characterRepo.GetASCIICharFromDec(color.G, includeControlCharacters));
                            rgbIndex.AddB(_characterRepo.GetASCIICharFromDec(color.B, includeControlCharacters));
                        }

                        AnsiConsole.WriteLine($"Red Text: {rgbIndex.R}");
                        AnsiConsole.WriteLine($"Green Text: {rgbIndex.G}");
                        AnsiConsole.WriteLine($"Blue Text: {rgbIndex.B}");

                        AnsiConsole.WriteLine($"Writing: ./output/imagep/RgbIndex_{file.PageName}.txt");
                        File.AppendAllText($"./output/imagep/RgbIndex_{file.PageName}.txt", "Red Text:" + rgbIndex.R + Environment.NewLine + Environment.NewLine);
                        File.AppendAllText($"./output/imagep/RgbIndex_{file.PageName}.txt", "Green Text: " + rgbIndex.G + Environment.NewLine + Environment.NewLine);
                        File.AppendAllText($"./output/imagep/RgbIndex_{file.PageName}.txt", "Blue Text: " + rgbIndex.B + Environment.NewLine + Environment.NewLine);
                    }
                });
            }
        }
    }
}