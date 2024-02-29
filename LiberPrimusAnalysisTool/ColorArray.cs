using System.Text;

namespace LiberPrimusAnalysisTool
{
    /// <summary>
    /// ColorArray - Gets the color counts in a list.
    /// </summary>
    internal class ColorArray
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorArray"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public ColorArray(string color)
        {
            Color = color;
            Lengths = new List<int>();
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the lengths.
        /// </summary>
        /// <value>
        /// The lengths.
        /// </value>
        public List<int> Lengths { get; set; }

        /// <summary>
        /// Converts to gemetria string.
        /// </summary>
        /// <returns></returns>
        public string ToGemetriaString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"GEME {Color}: ");
            foreach (var item in Lengths)
            {
                sb.Append(GemetriaUtilty.FromGematria(item));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts to num array spaced.
        /// </summary>
        /// <returns></returns>
        public string ToNumArraySpaced()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SPCE {Color}: ");
            foreach (var item in Lengths)
            {
                sb.Append($"{item} ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts to num array not spaced.
        /// </summary>
        /// <returns></returns>
        public string ToNumArrayNonSpaced()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"NSPC {Color}: ");
            foreach (var item in Lengths)
            {
                sb.Append(item);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts to char array.
        /// </summary>
        /// <returns></returns>
        public string ToCharArray()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UCHR {Color}: ");
            foreach (var item in Lengths)
            {
                try
                {
                    sb.Append(Convert.ToChar(item));
                }
                catch { }
            }

            return sb.ToString();
        }
    }
}
