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
            Sequence = new List<int>();
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the phi.
        /// </summary>
        /// <value>
        /// The phi.
        /// </value>
        public int Phi { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public List<int> Sequence { get; set; }

    }
}
