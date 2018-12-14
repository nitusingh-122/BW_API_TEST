using System;
using System.Data;
using System.Data.SqlClient;
using ASD.DataLayer;
using Logger;
using ASD.BW.Common;

namespace ASD.BW.SolutionOverview
{
    public class SolutionOverview
    {
       
        DataAccess objDA = new DataAccess();
        ErrorLog errlog = new ErrorLog();

        public DataTable GetSolutionOverviewData(string projectid)
        {
            DataSet dsSolutionOverviewData = new DataSet();
            DataTable dtSolutionOverviewData = new DataTable();
            
            try
            {
                String connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectSolutionOverview;

                SqlParameter[] paramCollection = new SqlParameter[1];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@ProjectID";
                paramCollection[0].Value = projectid;

              

                dsSolutionOverviewData = objDA.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsSolutionOverviewData.Tables.Count > 0)
                {
                    dtSolutionOverviewData = dsSolutionOverviewData.Tables[0];
                }

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetSolutionOverviewData");
            }
            return dtSolutionOverviewData;


        }


     
      
    }
}
