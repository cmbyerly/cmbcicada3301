using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// CODE_SET_TABLES
    /// </summary>
    public class CODE_SET_TABLES
    {
        /// <summary>
        /// Gets or sets the code set identifier.
        /// </summary>
        /// <value>
        /// The code set identifier.
        /// </value>
        [Key]
        public long CODE_SET_ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the code set.
        /// </summary>
        /// <value>
        /// The name of the code set.
        /// </value>
        [Required]
        [StringLength(50)]
        public string CODE_SET_NAME { get; set; }

        /// <summary>
        /// Gets or sets the code set character.
        /// </summary>
        /// <value>
        /// The code set character.
        /// </value>
        [Required]
        [StringLength(5)]
        public string CODE_SET_CHARACTER { get; set; }

        /// <summary>
        /// Gets or sets the code set decimal.
        /// </summary>
        /// <value>
        /// The code set decimal.
        /// </value>
        [Required]
        public long CODE_SET_DECIMAL { get; set; }

        /// <summary>
        /// Gets or sets the code set binary string.
        /// </summary>
        /// <value>
        /// The code set binary string.
        /// </value>
        [Required]
        [StringLength(255)]
        public string CODE_SET_BINARY_STRING { get; set; }
    }
}