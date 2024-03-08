using System.ComponentModel.DataAnnotations;

namespace LiberPrimusAnalysisTool.Database.DBEntity
{
    /// <summary>
    /// LINE_ORIENTATION
    /// </summary>
    public class LINE_ORIENTATION
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LINE_ORIENTATION"/> class.
        /// </summary>
        public LINE_ORIENTATION()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LINE_ORIENTATION"/> class.
        /// </summary>
        /// <param name="lINE_ORIENTATION_NAME">Name of the l ine orientation.</param>
        public LINE_ORIENTATION(string lINE_ORIENTATION_NAME)
        {
            LINE_ORIENTATION_NAME = lINE_ORIENTATION_NAME;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LINE_ORIENTATION"/> class.
        /// </summary>
        /// <param name="lINE_ORIENTATION_ID">The l ine orientation identifier.</param>
        /// <param name="lINE_ORIENTATION_NAME">Name of the l ine orientation.</param>
        public LINE_ORIENTATION(int lINE_ORIENTATION_ID, string lINE_ORIENTATION_NAME)
        {
            LINE_ORIENTATION_ID = lINE_ORIENTATION_ID;
            LINE_ORIENTATION_NAME = lINE_ORIENTATION_NAME;
        }

        /// <summary>
        /// Gets or sets the line orientation identifier.
        /// </summary>
        /// <value>
        /// The line orientation identifier.
        /// </value>
        [Key]
        public int LINE_ORIENTATION_ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the line orientation.
        /// </summary>
        /// <value>
        /// The name of the line orientation.
        /// </value>
        [Required]
        [StringLength(50)]
        public string LINE_ORIENTATION_NAME { get; set; }
    }
}