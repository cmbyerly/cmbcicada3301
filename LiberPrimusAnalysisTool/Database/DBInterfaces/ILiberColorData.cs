using LiberPrimusAnalysisTool.Entity;

namespace LiberPrimusAnalysisTool.Database.DBRepos
{
    /// <summary>
    /// ILiberColorData
    /// </summary>
    public interface ILiberColorData
    {
        /// <summary>
        /// Upserts the color of the liber.
        /// </summary>
        /// <param name="liberColor">Color of the liber.</param>
        void UpsertLiberColor(LiberColor liberColor);

        /// <summary>
        /// Inserts the color of the liber.
        /// </summary>
        /// <param name="liberColor">Color of the liber.</param>
        void InsertLiberColor(LiberColor liberColor);

        /// <summary>
        /// Updates the color of the liber.
        /// </summary>
        /// <param name="liberColor">Color of the liber.</param>
        void UpdateLiberColor(LiberColor liberColor);

        /// <summary>
        /// Gets the color of the liber.
        /// </summary>
        /// <param name="liberColorId">The liber color identifier.</param>
        /// <returns></returns>
        LiberColor GetLiberColor(string liberColorId);
    }
}