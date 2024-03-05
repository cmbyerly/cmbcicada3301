namespace LiberPrimusAnalysisTool.Database
{
    /// <summary>
    /// This is to get some database connection stuff.
    /// </summary>
    public class DatabaseConnectionUtils
    {
        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseConnectionString()
        {
            return $"Server={AppSettings.Default.DatabaseServer};Database={AppSettings.Default.Database};User Id={AppSettings.Default.UserName};Password={AppSettings.Default.Password};";
        }
    }
}
