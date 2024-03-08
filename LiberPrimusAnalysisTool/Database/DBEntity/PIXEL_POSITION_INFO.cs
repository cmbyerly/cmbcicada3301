using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// PIXEL_POSITION_INFO
    /// </summary>
    public class PIXEL_POSITION_INFO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PIXEL_POSITION_INFO"/> class.
        /// </summary>
        public PIXEL_POSITION_INFO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PIXEL_POSITION_INFO"/> class.
        /// </summary>
        /// <param name="pIXEL_POSITION_X">The p ixel position x.</param>
        /// <param name="pIXEL_POSITION_Y">The p ixel position y.</param>
        /// <param name="pIXEL_POSITION_CHANNELS">The p ixel position channels.</param>
        /// <param name="pIX_COLOR_ID">The p ix color identifier.</param>
        /// <param name="lIBER_PAGE_ID">The l iber page identifier.</param>
        public PIXEL_POSITION_INFO(long pIXEL_POSITION_X, long pIXEL_POSITION_Y, int pIXEL_POSITION_CHANNELS, long pIX_COLOR_ID, int lIBER_PAGE_ID)
        {
            PIXEL_POSITION_X = pIXEL_POSITION_X;
            PIXEL_POSITION_Y = pIXEL_POSITION_Y;
            PIXEL_POSITION_CHANNELS = pIXEL_POSITION_CHANNELS;
            PIX_COLOR_ID = pIX_COLOR_ID;
            LIBER_PAGE_ID = lIBER_PAGE_ID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PIXEL_POSITION_INFO"/> class.
        /// </summary>
        /// <param name="pIXEL_POSITION_INFO_ID">The p ixel position information identifier.</param>
        /// <param name="pIXEL_POSITION_X">The p ixel position x.</param>
        /// <param name="pIXEL_POSITION_Y">The p ixel position y.</param>
        /// <param name="pIXEL_POSITION_CHANNELS">The p ixel position channels.</param>
        /// <param name="pIX_COLOR_ID">The p ix color identifier.</param>
        /// <param name="lIBER_PAGE_ID">The l iber page identifier.</param>
        public PIXEL_POSITION_INFO(long pIXEL_POSITION_INFO_ID, long pIXEL_POSITION_X, long pIXEL_POSITION_Y, int pIXEL_POSITION_CHANNELS, long pIX_COLOR_ID, int lIBER_PAGE_ID)
        {
            PIXEL_POSITION_INFO_ID = pIXEL_POSITION_INFO_ID;
            PIXEL_POSITION_X = pIXEL_POSITION_X;
            PIXEL_POSITION_Y = pIXEL_POSITION_Y;
            PIXEL_POSITION_CHANNELS = pIXEL_POSITION_CHANNELS;
            PIX_COLOR_ID = pIX_COLOR_ID;
            LIBER_PAGE_ID = lIBER_PAGE_ID;
        }

        /// <summary>
        /// Gets or sets the pixel position information identifier.
        /// </summary>
        /// <value>
        /// The pixel position information identifier.
        /// </value>
        [Key]
        public long PIXEL_POSITION_INFO_ID { get; set; }

        /// <summary>
        /// Gets or sets the pixel position x.
        /// </summary>
        /// <value>
        /// The pixel position x.
        /// </value>
        [Required]
        public long PIXEL_POSITION_X { get; set; }

        /// <summary>
        /// Gets or sets the pixel position y.
        /// </summary>
        /// <value>
        /// The pixel position y.
        /// </value>
        [Required]
        public long PIXEL_POSITION_Y { get; set; }

        /// <summary>
        /// Gets or sets the pixel position channels.
        /// </summary>
        /// <value>
        /// The pixel position channels.
        /// </value>
        [Required]
        public int PIXEL_POSITION_CHANNELS { get; set; }

        /// <summary>
        /// Gets or sets the pix color identifier.
        /// </summary>
        /// <value>
        /// The pix color identifier.
        /// </value>
        [Required]
        public long PIX_COLOR_ID { get; set; }

        /// <summary>
        /// Gets or sets the liber page identifier.
        /// </summary>
        /// <value>
        /// The liber page identifier.
        /// </value>
        [Required]
        public int LIBER_PAGE_ID { get; set; }
    }
}