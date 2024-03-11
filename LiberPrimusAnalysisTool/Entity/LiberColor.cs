namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// A color used in the liber primus page.
    /// </summary>
    public class LiberColor
    {
        public LiberColor()
        {

        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long? LiberColorId { get; set; }

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