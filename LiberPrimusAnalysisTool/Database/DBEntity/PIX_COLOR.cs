using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// PIX_COLOR
    /// </summary>
    public class PIX_COLOR
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PIX_COLOR"/> class.
        /// </summary>
        public PIX_COLOR()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PIX_COLOR"/> class.
        /// </summary>
        /// <param name="pIX_COLOR_VALUE">The p ix color value.</param>
        public PIX_COLOR(string pIX_COLOR_VALUE)
        {
            PIX_COLOR_VALUE = pIX_COLOR_VALUE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PIX_COLOR"/> class.
        /// </summary>
        /// <param name="pIX_COLOR_ID">The p ix color identifier.</param>
        /// <param name="pIX_COLOR_VALUE">The p ix color value.</param>
        public PIX_COLOR(long pIX_COLOR_ID, string pIX_COLOR_VALUE)
        {
            PIX_COLOR_ID = pIX_COLOR_ID;
            PIX_COLOR_VALUE = pIX_COLOR_VALUE;
        }

        /// <summary>
        /// Gets or sets the pix color identifier.
        /// </summary>
        /// <value>
        /// The pix color identifier.
        /// </value>
        [Key]
        public long PIX_COLOR_ID { get; set; }

        /// <summary>
        /// Gets or sets the pix color value.
        /// </summary>
        /// <value>
        /// The pix color value.
        /// </value>
        [Required]
        [StringLength(10)]
        public string PIX_COLOR_VALUE { get; set; }
    }
}