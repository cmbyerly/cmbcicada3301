namespace LiberPrimusAnalysisTool.Utility.Math
{
    /// <summary>
    /// Utility for primes
    /// </summary>
    public static class PrimeUtility
    {
        /// <summary>
        /// Determines whether the specified value is prime.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPrime(int value)
        {
            if (value <= 1) return false;
            if (value == 2) return true;
            if (value % 2 == 0) return false;

            var boundary = (int)System.Math.Floor(System.Math.Sqrt(value));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
