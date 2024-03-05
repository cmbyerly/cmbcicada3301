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
        /// <param name="name">The name.</param>
        public LiberColor(string name)
        {
            Id = null;
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiberColor"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public LiberColor(long id, string name)
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
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}