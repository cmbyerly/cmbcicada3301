using Microsoft.Extensions.Configuration;

namespace LiberPrimusAnalysisTool.Database.DBRepos
{
    /// <summary>
    /// This is to get some database connection stuff.
    /// </summary>
    public class DatabaseConnectionUtils : IDatabaseConnectionUtils
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionUtils"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public DatabaseConnectionUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        /// <returns></returns>
        public string GetDatabaseConnectionString()
        {
            return $"Server={_configuration["DBServer"]};Database={_configuration["DBName"]};User Id={_configuration["DBUser"]};Password={_configuration["DBPassword"]};Encrypt=True;TrustServerCertificate=True";
        }
    }
}