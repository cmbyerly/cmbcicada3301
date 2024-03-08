namespace LiberPrimusAnalysisTool.Database.DBRepos
{
    /// <summary>
    /// This is to get some database connection stuff.
    /// </summary>
    public interface IDatabaseConnectionUtils
    {
        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        /// <returns></returns>
        string GetDatabaseConnectionString();
    }
}