using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// LIBER_COLOR
    /// </summary>
    public class LIBER_COLOR
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LIBER_COLOR"/> class.
        /// </summary>
        public LIBER_COLOR()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LIBER_COLOR"/> class.
        /// </summary>
        /// <param name="lIBER_PAGE_ID">The l iber page identifier.</param>
        /// <param name="pIX_COLOR_ID">The p ix color identifier.</param>
        /// <param name="lINE_ORIENTATION_ID">The l ine orientation identifier.</param>
        /// <param name="cOLOR_COUNT">The c olor count.</param>
        /// <param name="iS_PRIME">if set to <c>true</c> [i s prime].</param>
        /// <param name="gEMETRIA_VALUE">The g emetria value.</param>
        /// <param name="pHI_VALUE">The p hi value.</param>
        /// <param name="cHARACTER_VALUE">The c haracter value.</param>
        public LIBER_COLOR(int lIBER_PAGE_ID, long pIX_COLOR_ID, int lINE_ORIENTATION_ID, long cOLOR_COUNT, bool iS_PRIME, string gEMETRIA_VALUE, long? pHI_VALUE, string cHARACTER_VALUE)
        {
            LIBER_PAGE_ID = lIBER_PAGE_ID;
            PIX_COLOR_ID = pIX_COLOR_ID;
            LINE_ORIENTATION_ID = lINE_ORIENTATION_ID;
            COLOR_COUNT = cOLOR_COUNT;
            IS_PRIME = iS_PRIME;
            GEMETRIA_VALUE = gEMETRIA_VALUE;
            PHI_VALUE = pHI_VALUE;
            CHARACTER_VALUE = cHARACTER_VALUE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LIBER_COLOR"/> class.
        /// </summary>
        /// <param name="lIBER_COLOR_ID">The l iber color identifier.</param>
        /// <param name="lIBER_PAGE_ID">The l iber page identifier.</param>
        /// <param name="pIX_COLOR_ID">The p ix color identifier.</param>
        /// <param name="lINE_ORIENTATION_ID">The l ine orientation identifier.</param>
        /// <param name="cOLOR_COUNT">The c olor count.</param>
        /// <param name="iS_PRIME">if set to <c>true</c> [i s prime].</param>
        /// <param name="gEMETRIA_VALUE">The g emetria value.</param>
        /// <param name="pHI_VALUE">The p hi value.</param>
        /// <param name="cHARACTER_VALUE">The c haracter value.</param>
        public LIBER_COLOR(long lIBER_COLOR_ID, int lIBER_PAGE_ID, long pIX_COLOR_ID, int lINE_ORIENTATION_ID, long cOLOR_COUNT, bool iS_PRIME, string gEMETRIA_VALUE, long? pHI_VALUE, string cHARACTER_VALUE)
        {
            LIBER_COLOR_ID = lIBER_COLOR_ID;
            LIBER_PAGE_ID = lIBER_PAGE_ID;
            PIX_COLOR_ID = pIX_COLOR_ID;
            LINE_ORIENTATION_ID = lINE_ORIENTATION_ID;
            COLOR_COUNT = cOLOR_COUNT;
            IS_PRIME = iS_PRIME;
            GEMETRIA_VALUE = gEMETRIA_VALUE;
            PHI_VALUE = pHI_VALUE;
            CHARACTER_VALUE = cHARACTER_VALUE;
        }

        /// <summary>
        /// Gets or sets the liber color identifier.
        /// </summary>
        /// <value>
        /// The liber color identifier.
        /// </value>
        [Key]
        public long LIBER_COLOR_ID { get; set; }

        /// <summary>
        /// Gets or sets the liber page identifier.
        /// </summary>
        /// <value>
        /// The liber page identifier.
        /// </value>
        [Required]
        public int LIBER_PAGE_ID { get; set; }

        /// <summary>
        /// Gets or sets the pix color identifier.
        /// </summary>
        /// <value>
        /// The pix color identifier.
        /// </value>
        [Required]
        public long PIX_COLOR_ID { get; set; }

        /// <summary>
        /// Gets or sets the line orientation identifier.
        /// </summary>
        /// <value>
        /// The line orientation identifier.
        /// </value>
        [Required]
        public int LINE_ORIENTATION_ID { get; set; }

        /// <summary>
        /// Gets or sets the color count.
        /// </summary>
        /// <value>
        /// The color count.
        /// </value>
        [Required]
        public long COLOR_COUNT { get; set; }

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