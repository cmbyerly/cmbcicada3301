namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// The page from the liber primus
    /// </summary>
    public class LiberPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiberPage"/> class.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="pageSig">The page sig.</param>
        /// <param name="totalColors">The total colors.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        public LiberPage(string pageName, string pageSig, int totalColors, int height, int width)
        {
            Id = null;
            PageName = pageName;
            PageSig = pageSig;
            TotalColors = totalColors;
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiberPage"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="pageSig">The page sig.</param>
        /// <param name="totalColors">The total colors.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        public LiberPage(int id, string pageName, string pageSig, int totalColors, int height, int width)
        {
            Id = id;
            PageName = pageName;
            PageSig = pageSig;
            TotalColors = totalColors;
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>
        /// The name of the page.
        /// </value>
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets the page sig.
        /// </summary>
        /// <value>
        /// The page sig.
        /// </value>
        public string PageSig { get; set; }

        /// <summary>
        /// Gets or sets the total colors.
        /// </summary>
        /// <value>
        /// The total colors.
        /// </value>
        public int TotalColors { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }
    }
}