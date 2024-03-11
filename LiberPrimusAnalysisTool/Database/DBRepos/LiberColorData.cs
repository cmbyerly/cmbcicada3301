using LiberPrimusAnalysisTool.Database.Contexts;
using LiberPrimusAnalysisTool.Database.DBEntity;
using LiberPrimusAnalysisTool.Entity;

namespace LiberPrimusAnalysisTool.Database.DBRepos
{
    /// <summary>
    /// LiberColorData
    /// </summary>
    /// <seealso cref="LiberPrimusAnalysisTool.Database.DBRepos.ILiberColorData" />
    public class LiberColorData : ILiberColorData
    {
        private readonly IDatabaseConnectionUtils _databaseConnectionUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiberColorData"/> class.
        /// </summary>
        public LiberColorData(IDatabaseConnectionUtils databaseConnectionUtils)
        {
            _databaseConnectionUtils = databaseConnectionUtils;
        }

        /// <summary>
        /// Gets the color of the liber.
        /// </summary>
        /// <param name="liberColorHex">The liber color hexadecimal.</param>
        /// <returns></returns>
        public LiberColor GetLiberColor(string liberColorHex)
        {
            using(var context = new LiberContext(_databaseConnectionUtils))
            {
                var liberColor = context.PIX_COLOR.Where(x => x.PIX_COLOR_VALUE == liberColorHex).FirstOrDefault();
                if (liberColor == null)
                {
                    return null;
                }
                else
                {
                    return new LiberColor
                    {
                        LiberColorId = liberColor.PIX_COLOR_ID,
                        LiberColorHex = liberColor.PIX_COLOR_VALUE
                    };
                }
            }
        }

        /// <summary>
        /// Inserts the color of the liber.
        /// </summary>
        /// <param name="liberColor">Color of the liber.</param>
        public void InsertLiberColor(LiberColor liberColor)
        {
            using (var context = new LiberContext(_databaseConnectionUtils))
            {
                var newLiberColor = new PIX_COLOR
                {
                    PIX_COLOR_VALUE = liberColor.LiberColorHex
                };
                context.PIX_COLOR.Add(newLiberColor);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the color of the liber.
        /// </summary>
        /// <param name="liberColor">Color of the liber.</param>
        public void UpdateLiberColor(LiberColor liberColor)
        {
            using(var context = new LiberContext(_databaseConnectionUtils))
            {
                var existingLiberColor = context.PIX_COLOR.Where(x => x.PIX_COLOR_VALUE == liberColor.LiberColorHex).FirstOrDefault();
                if (existingLiberColor != null)
                {
                    existingLiberColor.PIX_COLOR_VALUE = liberColor.LiberColorHex;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Upserts the color of the liber.
        /// </summary>
        /// <param name="liberColor">Color of the liber.</param>
        public void UpsertLiberColor(LiberColor liberColor)
        {
            using(var context = new LiberContext(_databaseConnectionUtils))
            {
                var existingLiberColor = context.PIX_COLOR.Where(x => x.PIX_COLOR_VALUE == liberColor.LiberColorHex).FirstOrDefault();
                if (existingLiberColor == null)
                {
                    var newLiberColor = new PIX_COLOR
                    {
                        PIX_COLOR_VALUE = liberColor.LiberColorHex
                    };
                    context.PIX_COLOR.Add(newLiberColor);
                    context.SaveChanges();
                }
                else
                {
                    existingLiberColor.PIX_COLOR_VALUE = liberColor.LiberColorHex;
                    context.SaveChanges();
                }
            }
        }
    }
}