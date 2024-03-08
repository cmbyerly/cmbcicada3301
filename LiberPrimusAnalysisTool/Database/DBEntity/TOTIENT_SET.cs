using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// TOTIENT_SET
    /// </summary>
    public class TOTIENT_SET
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TOTIENT_SET"/> class.
        /// </summary>
        public TOTIENT_SET()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TOTIENT_SET"/> class.
        /// </summary>
        /// <param name="tOTIENT_NUMBER">The t otient number.</param>
        /// <param name="lINE_COLOR_INFO_ID">The l ine color information identifier.</param>
        public TOTIENT_SET(long tOTIENT_NUMBER, long lINE_COLOR_INFO_ID)
        {
            TOTIENT_NUMBER = tOTIENT_NUMBER;
            LINE_COLOR_INFO_ID = lINE_COLOR_INFO_ID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TOTIENT_SET"/> class.
        /// </summary>
        /// <param name="tOTIENT_SET_ID">The t otient set identifier.</param>
        /// <param name="tOTIENT_NUMBER">The t otient number.</param>
        /// <param name="lINE_COLOR_INFO_ID">The l ine color information identifier.</param>
        public TOTIENT_SET(long tOTIENT_SET_ID, long tOTIENT_NUMBER, long lINE_COLOR_INFO_ID)
        {
            TOTIENT_SET_ID = tOTIENT_SET_ID;
            TOTIENT_NUMBER = tOTIENT_NUMBER;
            LINE_COLOR_INFO_ID = lINE_COLOR_INFO_ID;
        }

        /// <summary>
        /// Gets or sets the totient set identifier.
        /// </summary>
        /// <value>
        /// The totient set identifier.
        /// </value>
        [Key]
        public long TOTIENT_SET_ID { get; set; }

        /// <summary>
        /// Gets or sets the totient number.
        /// </summary>
        /// <value>
        /// The totient number.
        /// </value>
        [Required]
        public long TOTIENT_NUMBER { get; set; }

        /// <summary>
        /// Gets or sets the line color information identifier.
        /// </summary>
        /// <value>
        /// The line color information identifier.
        /// </value>
        [Required]
        public long LINE_COLOR_INFO_ID { get; set; }
    }
}