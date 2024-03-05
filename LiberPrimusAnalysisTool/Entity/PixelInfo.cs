namespace LiberPrimusAnalysisTool.Entity
{
    /// <summary>
    /// This is the breakdown of the pixel by pixel information of a page.
    /// </summary>
    public class PixelInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PixelInfo"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="channels">The channels.</param>
        /// <param name="liberColor">Color of the liber.</param>
        /// <param name="liberPage">The liber page.</param>
        public PixelInfo(int x, int y, int channels, LiberColor liberColor, LiberPage liberPage)
        {
            Id = null;
            X = x;
            Y = y;
            Channels = channels;
            LiberColor = liberColor;
            LiberPage = liberPage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelInfo"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="channels">The channels.</param>
        /// <param name="liberColor">Color of the liber.</param>
        /// <param name="liberPage">The liber page.</param>
        public PixelInfo(long id, int x, int y, int channels, LiberColor liberColor, LiberPage liberPage)
        {
            Id = id;
            X = x;
            Y = y;
            Channels = channels;
            LiberColor = liberColor;
            LiberPage = liberPage;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        public int Channels { get; set; }

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
    }
}