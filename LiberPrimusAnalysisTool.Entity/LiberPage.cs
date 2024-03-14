namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// The page from the liber primus
    /// </summary>
    public class LiberPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiberPage"/> class.
        /// </summary>
        public LiberPage()
        {
            Colors = new List<LiberColor>();
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>
        /// The name of the page.
        /// </value>
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets the page sig.
        /// </summary>
        /// <value>
        /// The page sig.
        /// </value>
        public string PageSig { get; set; }

        /// <summary>
        /// Gets or sets the total colors.
        /// </summary>
        /// <value>
        /// The total colors.
        /// </value>
        public int TotalColors { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        /// <value>
        /// The colors.
        /// </value>
        public List<LiberColor> Colors { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{PageName} - ({FileName})";
        }
    }
}