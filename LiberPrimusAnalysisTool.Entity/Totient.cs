namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// Totient
    /// </summary>
    public class Totient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Totient" /> class.
        /// </summary>
        public Totient()
        {
            Sequence = new List<long>();
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public long Number { get; set; }

        /// <summary>
        /// Gets or sets the phi.
        /// </summary>
        /// <value>
        /// The phi.
        /// </value>
        public long Phi { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public List<long> Sequence { get; set; }
    }
}