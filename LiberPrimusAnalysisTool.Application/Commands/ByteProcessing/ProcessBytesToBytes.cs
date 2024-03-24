using LiberPrimusAnalysisTool.Entity;
using MediatR;
using Spectre.Console;
using System.Text;

namespace LiberPrimusAnalysisTool.Application.Commands.ByteProcessing
{
    /// <summary>
    /// ProcessBytesToBytes
    /// </summary>
    public class ProcessBytesToBytes
    {
        /// <summary>
        /// Command
        /// </summary>
        /// <seealso cref="IRequest" />
        public class Command : INotification
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Command" /> class.
            /// </summary>
            /// <param name="byteData">The byte data.</param>
            /// <param name="method">The method.</param>
            /// <param name="bitsOfSig">The bits of sig.</param>
            public Command(Tuple<LiberPage, List<byte>> byteData, string method, int bitsOfSig)
            {
                ByteData = byteData;
                Method = method;
                BitsOfSig = bitsOfSig;
            }

            /// <summary>
            /// Gets or sets the pixel data.
            /// </summary>
            /// <value>
            /// The pixel data.
            /// </value>
            public Tuple<LiberPage, List<byte>> ByteData { get; set; }

            /// <summary>
            /// Gets or sets the method.
            /// </summary>
            /// <value>
            /// The method.
            /// </value>
            public string Method { get; set; }

            /// <summary>
            /// Gets or sets the bits of sig.
            /// </summary>
            /// <value>
            /// The bits of sig.
            /// </value>
            public int BitsOfSig { get; set; }
        }

        /// <summary>
        /// Handler
        /// </summary>
        public class Handler : INotificationHandler<Command>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Handler" /> class.
            /// </summary>
            public Handler()
            {
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                AnsiConsole.WriteLine($"ProcessToBytes: Getting bits from {request.ByteData.Item1.PageName}");
                List<char> bits = new List<char>();

                foreach (var bytedata in request.ByteData.Item2)
                {
                    var bBits = Convert.ToString(bytedata, 2).PadLeft(8, '0');

                    var tmpBbits = bBits.Substring(8 - request.BitsOfSig, request.BitsOfSig);
                    foreach (var bit in tmpBbits)
                    {
                        bits.Add(bit);
                    }
                }

                AnsiConsole.WriteLine($"ProcessToBytes: Building bytes for bits for {request.ByteData.Item1.PageName}");
                List<byte> bytes = new List<byte>();
                StringBuilder ascii = new StringBuilder();
                foreach (var character in bits)
                {
                    ascii.Append(character);
                    if (ascii.Length >= 8)
                    {
                        bytes.Add(Convert.ToByte(ascii.ToString(), 2));
                        ascii.Clear();
                    }
                }

                AnsiConsole.WriteLine($"ProcessToBytes: Outputting bin file for {request.ByteData.Item1.PageName}");
                File.WriteAllBytes($"./output/bytep/{request.ByteData.Item1.PageName}-LSB-{request.Method}-{request.BitsOfSig}.bin", bytes.ToArray());
            }
        }
    }
}