using Spectre.Console;

namespace LiberPrimusAnalysisTool.Utility
{
    /// <summary>
    /// Logging Utility
    /// </summary>
    public static class LoggingUtility
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Log(string message)
        {
            AnsiConsole.MarkupLine($"LOG: [blue3_1]{message}[/]");
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogError(string message)
        {
            AnsiConsole.MarkupLine($"ERR: [red]{message}[/]");
        }
    }
}
