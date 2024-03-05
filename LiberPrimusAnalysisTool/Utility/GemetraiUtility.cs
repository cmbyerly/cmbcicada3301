namespace LiberPrimusAnalysisTool.Utility
{
    /// <summary>
    /// Converts a character to a gemetria character.
    /// </summary>
    internal class GemetriaUtilty
    {
        /// <summary>
        /// Froms the gematria.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string FromGematria(int value)
        {
            string retval = string.Empty;
            switch (value)
            {
                case 2:
                    retval = "F";
                    break;

                case 3:
                    retval = "U";
                    break;

                case 5:
                    retval = "TH";
                    break;

                case 7:
                    retval = "O";
                    break;

                case 11:
                    retval = "R";
                    break;

                case 13:
                    retval = "K";
                    break;

                case 17:
                    retval = "G";
                    break;

                case 19:
                    retval = "W";
                    break;

                case 23:
                    retval = "H";
                    break;

                case 29:
                    retval = "N";
                    break;

                case 31:
                    retval = "I";
                    break;

                case 37:
                    retval = "J";
                    break;

                case 41:
                    retval = "EO";
                    break;

                case 43:
                    retval = "P";
                    break;

                case 47:
                    retval = "X";
                    break;

                case 53:
                    retval = "Z";
                    break;

                case 59:
                    retval = "T";
                    break;

                case 61:
                    retval = "B";
                    break;

                case 67:
                    retval = "E";
                    break;

                case 71:
                    retval = "M";
                    break;

                case 73:
                    retval = "L";
                    break;

                case 79:
                    retval = "ING";
                    break;

                case 83:
                    retval = "OE";
                    break;

                case 89:
                    retval = "D";
                    break;

                case 97:
                    retval = "A";
                    break;

                case 101:
                    retval = "AE";
                    break;

                case 103:
                    retval = "Y";
                    break;

                case 107:
                    retval = "IA";
                    break;

                case 109:
                    retval = "EA";
                    break;

                default:
                    retval = string.Empty;
                    break;
            }

            return retval;
        }
    }
}