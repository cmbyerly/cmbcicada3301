namespace LiberPrimusAnalysisTool.Utility
{
    /// <summary>
    /// File utilities
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="file">The file.</param>
        public static void WriteToFile(IEnumerable<string> values, string file)
        {
            File.WriteAllLines($"{System.Environment.CurrentDirectory}/output/{file}", values);
        }

        /// <summary>
        /// Removes the previous runs.
        /// </summary>
        public static void RemovePreviousRuns()
        {
            if (!Directory.Exists($"{System.Environment.CurrentDirectory}/output"))
            {
                Directory.CreateDirectory($"{System.Environment.CurrentDirectory}/output");
            }

            var files = Directory.GetFiles($"{System.Environment.CurrentDirectory}/output");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}