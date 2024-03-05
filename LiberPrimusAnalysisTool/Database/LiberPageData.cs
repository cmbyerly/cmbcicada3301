using LiberPrimusAnalysisTool.Entity;
using System.Data;
using System.Data.SqlClient;

namespace LiberPrimusAnalysisTool.Database
{
    /// <summary>
    /// This is for manipulating liber primus page data.
    /// </summary>
    public class LiberPageData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiberPageData"/> class.
        /// </summary>
        public LiberPageData() 
        {    
        }

        /// <summary>
        /// Upserts the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        public int UpsertLiberPage(LiberPage liberPage)
        {
            if (liberPage.Id == null)
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
        private int InsertLiberPage(LiberPage liberPage)
        {
            int retval = 0;

            using(SqlConnection conn = new SqlConnection())
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[usp_LIBER_PAGEInsert]";

                SqlParameter parameterPageName = new()
                {
                    ParameterName = "@LIBER_PAGE_NAME",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterPageName);

                SqlParameter parameterSig = new()
                {
                    ParameterName = "@LIBER_PAGE_SIG",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterSig);

                SqlParameter parameterTotalColors = new()
                {
                    ParameterName = "@LIBER_PAGE_TOTAL_COLORS",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterTotalColors);

                SqlParameter parameterHeight = new()
                {
                    ParameterName = "@LIBER_PAGE_HEIGHT",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterHeight);

                SqlParameter parameterWidth = new()
                {
                    ParameterName = "@LIBER_PAGE_WIDTH",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterWidth);

                retval = (int)cmd.ExecuteScalar();

                conn.Close();
            }

            return retval;
        }

        /// <summary>
        /// Updates the liber page.
        /// </summary>
        /// <param name="liberPage">The liber page.</param>
        /// <returns></returns>
        private int UpdateLiberPage(LiberPage liberPage)
        {
            int retval = 0;

            using (SqlConnection conn = new SqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[usp_LIBER_COLORUpdate]";

                SqlParameter parameterId = new()
                {
                    ParameterName = "@LIBER_PAGE_ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.Id
                };
                cmd.Parameters.Add(parameterId);

                SqlParameter parameterPageName = new()
                {
                    ParameterName = "@LIBER_PAGE_NAME",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterPageName);

                SqlParameter parameterSig = new()
                {
                    ParameterName = "@LIBER_PAGE_SIG",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterSig);

                SqlParameter parameterTotalColors = new()
                {
                    ParameterName = "@LIBER_PAGE_TOTAL_COLORS",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterTotalColors);

                SqlParameter parameterHeight = new()
                {
                    ParameterName = "@LIBER_PAGE_HEIGHT",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterHeight);

                SqlParameter parameterWidth = new()
                {
                    ParameterName = "@LIBER_PAGE_WIDTH",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = liberPage.PageName
                };
                cmd.Parameters.Add(parameterWidth);

                retval = (int)cmd.ExecuteScalar();

                conn.Close();
            }

            return retval;
        }

    }
}
