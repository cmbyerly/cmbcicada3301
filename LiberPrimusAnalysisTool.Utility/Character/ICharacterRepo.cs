namespace LiberPrimusAnalysisTool.Utility.Character
{
    /// <summary>
    /// ICharacterRepo
    /// </summary>
    public interface ICharacterRepo
    {
        /// <summary>
        /// Gets the ANSI character from bin.
        /// </summary>
        /// <param name="bin">The bin.</param>
        /// <returns></returns>
        string GetANSICharFromBin(string bin, bool includeControlCharacters);

        /// <summary>
        /// Gets the ANSI character from decimal.
        /// </summary>
        /// <param name="dec">The decimal.</param>
        /// <returns></returns>
        string GetANSICharFromDec(int dec, bool includeControlCharacters);

        /// <summary>
        /// Gets the ASCII character from bin.
        /// </summary>
        /// <param name="bin">The bin.</param>
        /// <returns></returns>
        string GetASCIICharFromBin(string bin, bool includeControlCharacters);

        /// <summary>
        /// Gets the ASCII character from decimal.
        /// </summary>
        /// <param name="dec">The decimal.</param>
        /// <returns></returns>
        string GetASCIICharFromDec(int dec, bool includeControlCharacters);

        /// <summary>
        /// Gets the character from gematria value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        string GetCharacterFromGematriaValue(int value);

        /// <summary>
        /// Gets the value from string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        int GetValueFromString(string value);

        /// <summary>
        /// Gets the gematria strings.
        /// </summary>
        /// <returns></returns>
        string[] GetGematriaStrings();

        /// <summary>
        /// Gets the simple gematria strings.
        /// </summary>
        /// <returns></returns>
        string[] GetSimpleGematriaStrings();
    }
}