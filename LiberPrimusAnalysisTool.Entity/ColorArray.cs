using LiberPrimusAnalysisTool.Utility;
using LiberPrimusAnalysisTool.Utility.Math;
using System.Text;

namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// ColorArray - Gets the color counts in a list.
    /// </summary>
    public class ColorArray
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorArray" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="pageNumber">The page number.</param>
        public ColorArray(string color, string pageNumber)
        {
            PageNumber = pageNumber;
            Color = color;
            Lengths = new List<int>();
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public string PageNumber { get; set; }

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
        public string GemetriaString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"GEME {Color}: ");
                foreach (var item in Lengths)
                {
                    sb.Append(GemetriaUtilty.FromGematria(item));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Converts to num array spaced.
        /// </summary>
        /// <returns></returns>
        public string NumArraySpaced
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"SPCE {Color}: ");
                foreach (var item in Lengths)
                {
                    sb.Append($"{item} ");
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Converts to num array not spaced.
        /// </summary>
        /// <returns></returns>
        public string NumArrayNonSpaced
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"NSPC {Color}: ");
                foreach (var item in Lengths)
                {
                    sb.Append(item);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the ASCII character array.
        /// </summary>
        /// <value>
        /// The ASCII character array.
        /// </value>
        public string ASCIICharArray { get; set; }

        /// <summary>
        /// Gets or sets the ANSI character array.
        /// </summary>
        /// <value>
        /// The ANSI character array.
        /// </value>
        public string ANSICharArray { get; set; }

        /// <summary>
        /// Countses this instance.
        /// </summary>
        /// <returns></returns>
        public string ColorCount
        {
            get
            {
                int count = 0;
                foreach (var item in Lengths)
                {
                    count = count + item;
                }

                return $"Color: {Color} - {count} - Is Prime: {PrimeUtility.IsPrime(count)} - Number of contiguous instances: {Lengths.Count} - Is that prime: {PrimeUtility.IsPrime(Lengths.Count)}";
            }
        }

        /// <summary>
        /// Primes the array.
        /// </summary>
        /// <returns></returns>
        public string PrimeArray
        {
            get
            {
                return $"PRIMES: {StringUtilities.StringArrayPrimes(NumArraySpaced)}";
            }
        }
    }
}