using LiberPrimusAnalysisTool.Utility.Math;
using System.Text;

namespace LiberPrimusAnalysisTool.Utility
{
    /// <summary>
    /// String Utilities
    /// </summary>
    public static class StringUtilities
    {
        /// <summary>
        /// Removes the non alphanumeric characters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string RemoveNonAlphaNum(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in value)
            {
                if (char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Strings the array primes.
        /// </summary>
        /// <param name="arrayValue">The array value.</param>
        /// <returns></returns>
        public static string StringArrayPrimes(string arrayValue)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var numbers = arrayValue.Split(' ');

            foreach (var number in numbers)
            {
                try
                {
                    int testNumber = int.Parse(number.Trim());
                    if (PrimeUtility.IsPrime(testNumber))
                    {
                        stringBuilder.Append($"{testNumber} ");
                    }
                }
                catch { }
            }

            return stringBuilder.ToString();
        }
    }
}
