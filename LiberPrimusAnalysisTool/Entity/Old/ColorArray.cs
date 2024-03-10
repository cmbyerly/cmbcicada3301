using LiberPrimusAnalysisTool.Database.DBInterfaces;
using LiberPrimusAnalysisTool.Utility;
using LiberPrimusAnalysisTool.Utility.Math;
using System.Text;

namespace LiberPrimusAnalysisTool.Entity.Old
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
        public string ToASCIICharArray(ICharacterRepo characterRepo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"ASCII {Color}: ");
            foreach (var item in Lengths)
            {
                try
                {
                    sb.Append(characterRepo.GetCharacterByDecimal(item, "ASCII"));
                }
                catch { }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts to ansichararray.
        /// </summary>
        /// <param name="characterRepo">The character repo.</param>
        /// <returns></returns>
        public string ToANSICharArray(ICharacterRepo characterRepo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"ANSI {Color}: ");
            foreach (var item in Lengths)
            {
                try
                {
                    sb.Append(characterRepo.GetCharacterByDecimal(item, "ANSI"));
                }
                catch { }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Countses this instance.
        /// </summary>
        /// <returns></returns>
        public string ColorCount()
        {
            int count = 0;
            foreach (var item in Lengths)
            {
                count = count + item;
            }

            return $"Color: {Color} - {count} - Is Prime: {PrimeUtility.IsPrime(count)} - Number of contiguous instances: {Lengths.Count} - Is that prime: {PrimeUtility.IsPrime(Lengths.Count)}";
        }

        /// <summary>
        /// Primes the array.
        /// </summary>
        /// <returns></returns>
        public string ToPrimeArray()
        {
            return $"PRIMES: {StringUtilities.StringArrayPrimes(ToNumArraySpaced())}";
        }
    }
}