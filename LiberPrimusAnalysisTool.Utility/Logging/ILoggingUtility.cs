namespace LiberPrimusAnalysisTool.Utility.Logging
{
    public interface ILoggingUtility
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task Log(string message);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task LogError(string message);
    }
}