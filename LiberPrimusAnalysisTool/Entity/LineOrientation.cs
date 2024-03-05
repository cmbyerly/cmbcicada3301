namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// Whether the line data was for a vertical or horizontal read.
    /// </summary>
    public class LineOrientation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineOrientation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LineOrientation(string name)
        {
            Id = null;
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineOrientation"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public LineOrientation(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}