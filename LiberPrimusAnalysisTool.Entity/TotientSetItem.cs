namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// A member that constitutes the number phi.
    /// </summary>
    public class TotientSetItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TotientSetItem"/> class.
        /// </summary>
        /// <param name="number">The number.</param>
        public TotientSetItem(long number)
        {
            Id = null;
            Number = number;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TotientSetItem"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="number">The number.</param>
        public TotientSetItem(long id, long number)
        {
            Id = id;
            Number = number;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public long Number { get; set; }
    }
}