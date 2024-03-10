using LiberPrimusAnalysisTool.Database.Contexts;
using LiberPrimusAnalysisTool.Database.DBEntity;
using LiberPrimusAnalysisTool.Entity;
using System.Data;

namespace LiberPrimusAnalysisTool.Database.DBRepos
{
    /// <summary>
    /// This is for manipulating liber primus page data.
    /// </summary>
    public class LiberPageData : ILiberPageData
    {
        private readonly IDatabaseConnectionUtils _databaseConnectionUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiberPageData"/> class.
        /// </summary>
        public LiberPageData(IDatabaseConnectionUtils databaseConnectionUtils)
        {
            _databaseConnectionUtils = databaseConnectionUtils;
        }

        /// <summary>
        /// Upserts the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        public int UpsertLiberPage(LiberPage liberPage)
        {
            var page = GetLiberPage(liberPage.PageName);
            if (page == null || page.Id == null)
            {
                return InsertLiberPage(liberPage);
            }
            else
            {
                return UpdateLiberPage(liberPage);
            }
        }

        /// <summary>
        /// Inserts the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        public int InsertLiberPage(LiberPage liberPage)
        {
            int retval = 0;

            using (var context = new LiberContext(_databaseConnectionUtils))
            {
                var liberPageEntity = new LIBER_PAGE
                {
                    LIBER_PAGE_NAME = liberPage.PageName,
                    LIBER_PAGE_SIG = liberPage.PageSig,
                    LIBER_PAGE_TOTAL_COLORS = liberPage.TotalColors,
                    LIBER_PAGE_HEIGHT = liberPage.Height,
                    LIBER_PAGE_WIDTH = liberPage.Width
                };

                context.LIBER_PAGE.Add(liberPageEntity);
                retval = context.SaveChanges();
            }
            
            return retval;
        }

        /// <summary>
        /// Updates the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        public int UpdateLiberPage(LiberPage liberPage)
        {
            int retval = 0;

            using (var context = new LiberContext(_databaseConnectionUtils))
            {
                var liberPageEntity = context.LIBER_PAGE.Where(x => x.LIBER_PAGE_NAME == liberPage.PageName).First();

                liberPageEntity.LIBER_PAGE_NAME = liberPage.PageName;
                liberPageEntity.LIBER_PAGE_SIG = liberPage.PageSig;
                liberPageEntity.LIBER_PAGE_TOTAL_COLORS = liberPage.TotalColors;
                liberPageEntity.LIBER_PAGE_HEIGHT = liberPage.Height;
                liberPageEntity.LIBER_PAGE_WIDTH = liberPage.Width;

                context.LIBER_PAGE.Add(liberPageEntity);
                retval = context.SaveChanges();
            }

            return retval;
        }

        /// <summary>
        /// Gets the liber page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns></returns>
        public LiberPage GetLiberPage(string pageName)
        {
            LiberPage retval = null;

            using (var context = new LiberContext(_databaseConnectionUtils))
            {
                var liberPageEntity = context.LIBER_PAGE.Where(x => x.LIBER_PAGE_NAME == pageName).FirstOrDefault();

                if (liberPageEntity != null)
                {
                    retval = new LiberPage
                    {
                        Id = liberPageEntity.LIBER_PAGE_ID,
                        PageName = liberPageEntity.LIBER_PAGE_NAME,
                        PageSig = liberPageEntity.LIBER_PAGE_SIG,
                        TotalColors = liberPageEntity.LIBER_PAGE_TOTAL_COLORS,
                        Height = liberPageEntity.LIBER_PAGE_HEIGHT,
                        Width = liberPageEntity.LIBER_PAGE_WIDTH
                    };
                }
            }

            return retval;
        }
    }
}