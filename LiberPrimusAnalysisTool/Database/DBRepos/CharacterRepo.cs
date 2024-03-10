using LiberPrimusAnalysisTool.Database.Contexts;
using LiberPrimusAnalysisTool.Database.DBInterfaces;

namespace LiberPrimusAnalysisTool.Database.DBRepos
{
    /// <summary>
    /// CharacterRepo
    /// </summary>
    /// <seealso cref="LiberPrimusAnalysisTool.Database.DBInterfaces.ICharacterRepo" />
    public class CharacterRepo : ICharacterRepo
    {
        /// <summary>
        /// The database connection utils
        /// </summary>
        private readonly IDatabaseConnectionUtils _databaseConnectionUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterRepo"/> class.
        /// </summary>
        /// <param name="databaseConnectionUtils">The database connection utils.</param>
        public CharacterRepo(IDatabaseConnectionUtils databaseConnectionUtils)
        {
            _databaseConnectionUtils = databaseConnectionUtils;
        }

        /// <summary>
        /// Characters the by bin.
        /// </summary>
        /// <param name="binary">The binary.</param>
        /// <param name="characterSet">The character set.</param>
        /// <returns></returns>
        public string CharacterByBin(string binary, string characterSet)
        {
            string retval = string.Empty;
            using (var context = new LiberContext(_databaseConnectionUtils))
            {
                var codeSet = context.CODE_SET_TABLES.FirstOrDefault(x => x.CODE_SET_BINARY_STRING == binary && x.CODE_SET_NAME == characterSet);
                if (codeSet != null)
                {
                    retval = codeSet.CODE_SET_CHARACTER;
                }
            }
            return retval;
        }

        /// <summary>
        /// Gets the character by decimal.
        /// </summary>
        /// <param name="decimalValue">The decimal value.</param>
        /// <param name="characterSet">The character set.</param>
        /// <returns></returns>
        public string GetCharacterByDecimal(long decimalValue, string characterSet)
        {
            string retval = string.Empty;
            using (var context = new LiberContext(_databaseConnectionUtils))
            {
                var codeSet = context.CODE_SET_TABLES.FirstOrDefault(x => x.CODE_SET_DECIMAL == decimalValue && x.CODE_SET_NAME == characterSet);
                if (codeSet != null)
                {
                    retval = codeSet.CODE_SET_CHARACTER;
                }
            }
            return retval;
        }
    }
}