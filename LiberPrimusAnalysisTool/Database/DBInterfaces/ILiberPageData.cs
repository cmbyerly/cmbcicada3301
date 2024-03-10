using LiberPrimusAnalysisTool.Entity;

namespace LiberPrimusAnalysisTool.Database.DBRepos
{
    /// <summary>
    /// This is for manipulating liber primus page data.
    /// </summary>
    public interface ILiberPageData
    {
        /// <summary>
        /// Upserts the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        int UpsertLiberPage(LiberPage liberPage);

        /// <summary>
        /// Inserts the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        int InsertLiberPage(LiberPage liberPage);

        /// <summary>
        /// Updates the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        int UpdateLiberPage(LiberPage liberPage);

        /// <summary>
        /// Gets the liber page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns></returns>
        LiberPage GetLiberPage(string pageName);
    }
}