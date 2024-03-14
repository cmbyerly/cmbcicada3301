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