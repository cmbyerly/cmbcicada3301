using LiberPrimusAnalysisTool.Database.DBEntity;
using LiberPrimusAnalysisTool.Database.DBRepos;
using Microsoft.EntityFrameworkCore;

namespace LiberPrimusAnalysisTool.Database.Contexts
{
    /// <summary>
    /// LiberContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class LiberContext : DbContext
    {
        /// <summary>
        /// The database connection utils
        /// </summary>
        private readonly IDatabaseConnectionUtils _databaseConnectionUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiberContext"/> class.
        /// </summary>
        /// <param name="databaseConnectionUtils">The database connection utils.</param>
        public LiberContext(IDatabaseConnectionUtils databaseConnectionUtils)
        {
            _databaseConnectionUtils = databaseConnectionUtils;
        }

        /// <summary>
        /// Gets or sets the color totient set.
        /// </summary>
        /// <value>
        /// The color totient set.
        /// </value>
        public DbSet<COLOR_TOTIENT_SET> COLOR_TOTIENT_SET { get; set; }

        /// <summary>
        /// Gets or sets the color of the liber.
        /// </summary>
        /// <value>
        /// The color of the liber.
        /// </value>
        public DbSet<LIBER_COLOR> LIBER_COLOR { get; set; }

        /// <summary>
        /// Gets or sets the liber page.
        /// </summary>
        /// <value>
        /// The liber page.
        /// </value>
        public DbSet<LIBER_PAGE> LIBER_PAGE { get; set; }

        /// <summary>
        /// Gets or sets the line color information.
        /// </summary>
        /// <value>
        /// The line color information.
        /// </value>
        public DbSet<LINE_COLOR_INFO> LINE_COLOR_INFO { get; set; }

        /// <summary>
        /// Gets or sets the line orientation.
        /// </summary>
        /// <value>
        /// The line orientation.
        /// </value>
        public DbSet<LINE_ORIENTATION> LINE_ORIENTATION { get; set; }

        /// <summary>
        /// Gets or sets the color of the pix.
        /// </summary>
        /// <value>
        /// The color of the pix.
        /// </value>
        public DbSet<PIX_COLOR> PIX_COLOR { get; set; }

        /// <summary>
        /// Gets or sets the pixel position information.
        /// </summary>
        /// <value>
        /// The pixel position information.
        /// </value>
        public DbSet<PIXEL_POSITION_INFO> PIXEL_POSITION_INFO { get; set; }

        /// <summary>
        /// Gets or sets the totient set.
        /// </summary>
        /// <value>
        /// The totient set.
        /// </value>
        public DbSet<TOTIENT_SET> TOTIENT_SET { get; set; }

        /// <summary>
        /// Gets or sets the code set tables.
        /// </summary>
        /// <value>
        /// The code set tables.
        /// </value>
        public DbSet<CODE_SET_TABLES> CODE_SET_TABLES { get; set; }

        /// <summary>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        /// <remarks>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
        /// for more information and examples.
        /// </para>
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseConnectionUtils.GetDatabaseConnectionString());
        }
    }
}