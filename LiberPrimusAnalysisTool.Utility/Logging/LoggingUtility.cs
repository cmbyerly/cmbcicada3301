using Microsoft.Extensions.Configuration;
using Nest;
using Spectre.Console;

namespace LiberPrimusAnalysisTool.Utility.Logging
{
    /// <summary>
    /// Logging Utility
    /// </summary>
    public class LoggingUtility : ILoggingUtility
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// The elastic client
        /// </summary>
        private readonly ElasticClient _elasticClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionUtils"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public LoggingUtility(IConfiguration configuration)
        {
            _configuration = configuration;
            var connSettings = new ConnectionSettings(new Uri($"http://{_configuration["ElkServer"]}:{_configuration["ElkPort"]}"));
            connSettings.DefaultIndex("log");
            _elasticClient = new ElasticClient(connSettings);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public async Task Log(string message)
        {
            AnsiConsole.MarkupLine($"LOG: [blue3_1]{message}[/]");
            _elasticClient.IndexDocument(new ElkLogEntity
            {
                Message = message,
                LogType = "LOG",
                Timestamp = DateTime.Now
            });
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        public async Task LogError(string message)
        {
            AnsiConsole.MarkupLine($"ERR: [red]{message}[/]");
            _elasticClient.IndexDocument(new ElkLogEntity
            {
                Message = message,
                LogType = "ERROR_LOG",
                Timestamp = DateTime.Now
            });
        }
    }
}