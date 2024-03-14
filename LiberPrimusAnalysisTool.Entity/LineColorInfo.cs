namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// This is the color information when we break it down by line.
    /// </summary>
    public class LineColorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineColorInfo"/> class.
        /// </summary>
        public LineColorInfo()
        {
            TotientItems = new List<TotientSetItem>();
        }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public long LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the color of the liber.
        /// </summary>
        /// <value>
        /// The color of the liber.
        /// </value>
        public LiberColor LiberColor { get; set; }

        /// <summary>
        /// Gets or sets the liber page.
        /// </summary>
        /// <value>
        /// The liber page.
        /// </value>
        public LiberPage LiberPage { get; set; }

        /// <summary>
        /// Gets or sets the line orientation.
        /// </summary>
        /// <value>
        /// The line orientation.
        /// </value>
        public string LineOrientation { get; set; }

        /// <summary>
        /// Gets or sets the line color count.
        /// </summary>
        /// <value>
        /// The line color count.
        /// </value>
        public int LineColorCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is prime.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is prime; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrime { get; set; }

        /// <summary>
        /// Gets or sets the gemetria value.
        /// </summary>
        /// <value>
        /// The gemetria value.
        /// </value>
        public string GemetriaValue { get; set; }

        /// <summary>
        /// Gets or sets the phi value.
        /// </summary>
        /// <value>
        /// The phi value.
        /// </value>
        public long? PhiValue { get; set; }

        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        /// <value>
        /// The character value.
        /// </value>
        public string CharacterValue { get; set; }

        /// <summary>
        /// Gets or sets the totient items.
        /// </summary>
        /// <value>
        /// The totient items.
        /// </value>
        public List<TotientSetItem> TotientItems { get; set; }
    }
}