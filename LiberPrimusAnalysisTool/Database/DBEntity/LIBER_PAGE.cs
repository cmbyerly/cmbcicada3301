using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// LIBER_PAGE
    /// </summary>
    public class LIBER_PAGE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LIBER_PAGE"/> class.
        /// </summary>
        public LIBER_PAGE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LIBER_PAGE"/> class.
        /// </summary>
        /// <param name="lIBER_PAGE_NAME">Name of the l iber page.</param>
        /// <param name="lIBER_PAGE_SIG">The l iber page sig.</param>
        /// <param name="lIBER_PAGE_TOTAL_COLORS">The l iber page total colors.</param>
        /// <param name="lIBER_PAGE_HEIGHT">Height of the l iber page.</param>
        /// <param name="lIBER_PAGE_WIDTH">Width of the l iber page.</param>
        public LIBER_PAGE(string lIBER_PAGE_NAME, string lIBER_PAGE_SIG, int lIBER_PAGE_TOTAL_COLORS, int lIBER_PAGE_HEIGHT, int lIBER_PAGE_WIDTH)
        {
            LIBER_PAGE_NAME = lIBER_PAGE_NAME;
            LIBER_PAGE_SIG = lIBER_PAGE_SIG;
            LIBER_PAGE_TOTAL_COLORS = lIBER_PAGE_TOTAL_COLORS;
            LIBER_PAGE_HEIGHT = lIBER_PAGE_HEIGHT;
            LIBER_PAGE_WIDTH = lIBER_PAGE_WIDTH;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LIBER_PAGE"/> class.
        /// </summary>
        /// <param name="lIBER_PAGE_ID">The l iber page identifier.</param>
        /// <param name="lIBER_PAGE_NAME">Name of the l iber page.</param>
        /// <param name="lIBER_PAGE_SIG">The l iber page sig.</param>
        /// <param name="lIBER_PAGE_TOTAL_COLORS">The l iber page total colors.</param>
        /// <param name="lIBER_PAGE_HEIGHT">Height of the l iber page.</param>
        /// <param name="lIBER_PAGE_WIDTH">Width of the l iber page.</param>
        public LIBER_PAGE(int lIBER_PAGE_ID, string lIBER_PAGE_NAME, string lIBER_PAGE_SIG, int lIBER_PAGE_TOTAL_COLORS, int lIBER_PAGE_HEIGHT, int lIBER_PAGE_WIDTH)
        {
            LIBER_PAGE_ID = lIBER_PAGE_ID;
            LIBER_PAGE_NAME = lIBER_PAGE_NAME;
            LIBER_PAGE_SIG = lIBER_PAGE_SIG;
            LIBER_PAGE_TOTAL_COLORS = lIBER_PAGE_TOTAL_COLORS;
            LIBER_PAGE_HEIGHT = lIBER_PAGE_HEIGHT;
            LIBER_PAGE_WIDTH = lIBER_PAGE_WIDTH;
        }

        /// <summary>
        /// Gets or sets the liber page identifier.
        /// </summary>
        /// <value>
        /// The liber page identifier.
        /// </value>
        [Key]
        public int LIBER_PAGE_ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the liber page.
        /// </summary>
        /// <value>
        /// The name of the liber page.
        /// </value>
        [Required]
        [StringLength(50)]
        public string LIBER_PAGE_NAME { get; set; }

        /// <summary>
        /// Gets or sets the liber page sig.
        /// </summary>
        /// <value>
        /// The liber page sig.
        /// </value>
        [Required]
        [StringLength(255)]
        public string LIBER_PAGE_SIG { get; set; }

        /// <summary>
        /// Gets or sets the liber page total colors.
        /// </summary>
        /// <value>
        /// The liber page total colors.
        /// </value>
        [Required]
        public int LIBER_PAGE_TOTAL_COLORS { get; set; }

        /// <summary>
        /// Gets or sets the height of the liber page.
        /// </summary>
        /// <value>
        /// The height of the liber page.
        /// </value>
        [Required]
        public int LIBER_PAGE_HEIGHT { get; set; }

        /// <summary>
        /// Gets or sets the width of the liber page.
        /// </summary>
        /// <value>
        /// The width of the liber page.
        /// </value>
        [Required]
        public int LIBER_PAGE_WIDTH { get; set; }
    }
}