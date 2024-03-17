namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// A color used in the liber primus page.
    /// </summary>
    public class LiberColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiberColor"/> class.
        /// </summary>
        public LiberColor()
        {
        }

        /// <summary>
        /// Gets or sets the hexadecimal.
        /// </summary>
        /// <value>
        /// The hexadecimal.
        /// </value>
        public string LiberColorHex { get; set; }

        /// <summary>
        /// Gets the liber color hashless.
        /// </summary>
        /// <value>
        /// The liber color hashless.
        /// </value>
        public string LiberColorHashless
        {
            get
            {
                return LiberColorHex.Replace("#", string.Empty);
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return LiberColorHex;
        }
    }
}