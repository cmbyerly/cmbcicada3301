using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// LINE_COLOR_INFO
    /// </summary>
    public class LINE_COLOR_INFO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LINE_COLOR_INFO"/> class.
        /// </summary>
        public LINE_COLOR_INFO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LINE_COLOR_INFO"/> class.
        /// </summary>
        /// <param name="lINE_NUMBER">The l ine number.</param>
        /// <param name="pIX_COLOR_ID">The p ix color identifier.</param>
        /// <param name="lIBER_PAGE_ID">The l iber page identifier.</param>
        /// <param name="lINE_ORIENTATION_ID">The l ine orientation identifier.</param>
        /// <param name="lINE_COLOR_COUNT">The l ine color count.</param>
        /// <param name="iS_PRIME">if set to <c>true</c> [i s prime].</param>
        /// <param name="gEMETRIA_VALUE">The g emetria value.</param>
        /// <param name="pHI_VALUE">The p hi value.</param>
        /// <param name="cHARACTER_VALUE">The c haracter value.</param>
        public LINE_COLOR_INFO(long lINE_NUMBER, long pIX_COLOR_ID, int lIBER_PAGE_ID, int lINE_ORIENTATION_ID, int lINE_COLOR_COUNT, bool iS_PRIME, string gEMETRIA_VALUE, long? pHI_VALUE, string cHARACTER_VALUE)
        {
            LINE_NUMBER = lINE_NUMBER;
            PIX_COLOR_ID = pIX_COLOR_ID;
            LIBER_PAGE_ID = lIBER_PAGE_ID;
            LINE_ORIENTATION_ID = lINE_ORIENTATION_ID;
            LINE_COLOR_COUNT = lINE_COLOR_COUNT;
            IS_PRIME = iS_PRIME;
            GEMETRIA_VALUE = gEMETRIA_VALUE;
            PHI_VALUE = pHI_VALUE;
            CHARACTER_VALUE = cHARACTER_VALUE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LINE_COLOR_INFO"/> class.
        /// </summary>
        /// <param name="lINE_COLOR_INFO_ID">The l ine color information identifier.</param>
        /// <param name="lINE_NUMBER">The l ine number.</param>
        /// <param name="pIX_COLOR_ID">The p ix color identifier.</param>
        /// <param name="lIBER_PAGE_ID">The l iber page identifier.</param>
        /// <param name="lINE_ORIENTATION_ID">The l ine orientation identifier.</param>
        /// <param name="lINE_COLOR_COUNT">The l ine color count.</param>
        /// <param name="iS_PRIME">if set to <c>true</c> [i s prime].</param>
        /// <param name="gEMETRIA_VALUE">The g emetria value.</param>
        /// <param name="pHI_VALUE">The p hi value.</param>
        /// <param name="cHARACTER_VALUE">The c haracter value.</param>
        public LINE_COLOR_INFO(long lINE_COLOR_INFO_ID, long lINE_NUMBER, long pIX_COLOR_ID, int lIBER_PAGE_ID, int lINE_ORIENTATION_ID, int lINE_COLOR_COUNT, bool iS_PRIME, string gEMETRIA_VALUE, long? pHI_VALUE, string cHARACTER_VALUE)
        {
            LINE_COLOR_INFO_ID = lINE_COLOR_INFO_ID;
            LINE_NUMBER = lINE_NUMBER;
            PIX_COLOR_ID = pIX_COLOR_ID;
            LIBER_PAGE_ID = lIBER_PAGE_ID;
            LINE_ORIENTATION_ID = lINE_ORIENTATION_ID;
            LINE_COLOR_COUNT = lINE_COLOR_COUNT;
            IS_PRIME = iS_PRIME;
            GEMETRIA_VALUE = gEMETRIA_VALUE;
            PHI_VALUE = pHI_VALUE;
            CHARACTER_VALUE = cHARACTER_VALUE;
        }

        /// <summary>
        /// Gets or sets the line color information identifier.
        /// </summary>
        /// <value>
        /// The line color information identifier.
        /// </value>
        [Key]
        public long LINE_COLOR_INFO_ID { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        [Required]
        public long LINE_NUMBER { get; set; }

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

        /// <summary>
        /// Gets or sets the line orientation identifier.
        /// </summary>
        /// <value>
        /// The line orientation identifier.
        /// </value>
        [Required]
        public int LINE_ORIENTATION_ID { get; set; }

        /// <summary>
        /// Gets or sets the line color count.
        /// </summary>
        /// <value>
        /// The line color count.
        /// </value>
        [Required]
        public int LINE_COLOR_COUNT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is prime.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is prime; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IS_PRIME { get; set; }

        /// <summary>
        /// Gets or sets the gemetria value.
        /// </summary>
        /// <value>
        /// The gemetria value.
        /// </value>
        [StringLength(5)]
        public string GEMETRIA_VALUE { get; set; }

        /// <summary>
        /// Gets or sets the phi value.
        /// </summary>
        /// <value>
        /// The phi value.
        /// </value>
        public long? PHI_VALUE { get; set; }

        /// <summary>
        /// Gets or sets the character value.
        /// </summary>
        /// <value>
        /// The character value.
        /// </value>
        [StringLength(5)]
        public string CHARACTER_VALUE { get; set; }
    }
}