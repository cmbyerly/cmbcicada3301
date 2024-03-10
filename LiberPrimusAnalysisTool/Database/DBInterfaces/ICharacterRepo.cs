namespace LiberPrimusAnalysisTool.Database.DBInterfaces
{
    /// <summary>
    /// ICharacterRepo
    /// </summary>
    public interface ICharacterRepo
    {
        /// <summary>
        /// Gets the character by decimal.
        /// </summary>
        /// <param name="decimalValue">The decimal value.</param>
        /// <param name="characterSet">The character set.</param>
        /// <returns></returns>
        public string GetCharacterByDecimal(long decimalValue, string characterSet);

        /// <summary>
        /// Characters the by bin.
        /// </summary>
        /// <param name="binary">The binary.</param>
        /// <param name="characterSet">The character set.</param>
        /// <returns></returns>
        public string CharacterByBin(string binary, string characterSet);
    }
}