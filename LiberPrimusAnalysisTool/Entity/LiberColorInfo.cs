namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// This is when we do the color information without the line.
    /// </summary>
    public class LiberColorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiberColorInfo"/> class.
        /// </summary>
        /// <param name="liberColor">Color of the liber.</param>
        /// <param name="liberPage">The liber page.</param>
        /// <param name="lineOrientation">The line orientation.</param>
        /// <param name="colorCount">The color count.</param>
        /// <param name="isPrime">if set to <c>true</c> [is prime].</param>
        /// <param name="gemetriaValue">The gemetria value.</param>
        /// <param name="phiValue">The phi value.</param>
        /// <param name="characterValue">The character value.</param>
        public LiberColorInfo(LiberColor liberColor, LiberPage liberPage, LineOrientation lineOrientation, int colorCount, bool isPrime, string gemetriaValue, long phiValue, string characterValue)
        {
            Id = null;
            LiberColor = liberColor;
            LiberPage = liberPage;
            LineOrientation = lineOrientation;
            ColorCount = colorCount;
            IsPrime = isPrime;
            GemetriaValue = gemetriaValue;
            PhiValue = phiValue;
            CharacterValue = characterValue;
            TotientItems = new List<TotientSetItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiberColorInfo"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="liberColor">Color of the liber.</param>
        /// <param name="liberPage">The liber page.</param>
        /// <param name="lineOrientation">The line orientation.</param>
        /// <param name="colorCount">The color count.</param>
        /// <param name="isPrime">if set to <c>true</c> [is prime].</param>
        /// <param name="gemetriaValue">The gemetria value.</param>
        /// <param name="phiValue">The phi value.</param>
        /// <param name="characterValue">The character value.</param>
        public LiberColorInfo(long id, LiberColor liberColor, LiberPage liberPage, LineOrientation lineOrientation, int colorCount, bool isPrime, string gemetriaValue, long phiValue, string characterValue)
        {
            Id = id;
            LiberColor = liberColor;
            LiberPage = liberPage;
            LineOrientation = lineOrientation;
            ColorCount = colorCount;
            IsPrime = isPrime;
            GemetriaValue = gemetriaValue;
            PhiValue = phiValue;
            CharacterValue = characterValue;
            TotientItems = new List<TotientSetItem>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long? Id { get; set; }

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
        public LineOrientation LineOrientation { get; set; }

        /// <summary>
        /// Gets or sets the color count.
        /// </summary>
        /// <value>
        /// The color count.
        /// </value>
        public int ColorCount { get; set; }

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
        public long PhiValue { get; set; }

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