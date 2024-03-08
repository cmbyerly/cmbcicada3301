using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// COLOR_TOTIENT_SET
    /// </summary>
    public class COLOR_TOTIENT_SET
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="COLOR_TOTIENT_SET"/> class.
        /// </summary>
        public COLOR_TOTIENT_SET()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="COLOR_TOTIENT_SET"/> class.
        /// </summary>
        /// <param name="tOTIENT_NUMBER">The t otient number.</param>
        /// <param name="lIBER_COLOR_ID">The l iber color identifier.</param>
        public COLOR_TOTIENT_SET(long tOTIENT_NUMBER, long lIBER_COLOR_ID)
        {
            TOTIENT_NUMBER = tOTIENT_NUMBER;
            LIBER_COLOR_ID = lIBER_COLOR_ID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="COLOR_TOTIENT_SET"/> class.
        /// </summary>
        /// <param name="cOLOR_TOTIENT_SET_ID">The c olor totient set identifier.</param>
        /// <param name="tOTIENT_NUMBER">The t otient number.</param>
        /// <param name="lIBER_COLOR_ID">The l iber color identifier.</param>
        public COLOR_TOTIENT_SET(long cOLOR_TOTIENT_SET_ID, long tOTIENT_NUMBER, long lIBER_COLOR_ID)
        {
            COLOR_TOTIENT_SET_ID = cOLOR_TOTIENT_SET_ID;
            TOTIENT_NUMBER = tOTIENT_NUMBER;
            LIBER_COLOR_ID = lIBER_COLOR_ID;
        }

        /// <summary>
        /// Gets or sets the color totient set identifier.
        /// </summary>
        /// <value>
        /// The color totient set identifier.
        /// </value>
        [Key]
        public long COLOR_TOTIENT_SET_ID { get; set; }

        /// <summary>
        /// Gets or sets the totient number.
        /// </summary>
        /// <value>
        /// The totient number.
        /// </value>
        [Required]
        public long TOTIENT_NUMBER { get; set; }

        /// <summary>
        /// Gets or sets the liber color identifier.
        /// </summary>
        /// <value>
        /// The liber color identifier.
        /// </value>
        [Required]
        public long LIBER_COLOR_ID { get; set; }
    }
}