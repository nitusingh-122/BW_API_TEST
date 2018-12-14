using ASD.ApplicationService.Models.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ASD.ApplicationService.Models.DataAccess
{
    public class DataRepository
    {
        public bool VerifyAccess(String userid, String password)
        {
            String result = String.Empty;
            try
            {
                String ConnectionString = ApplicationCommon.GetAiesDbConnectionString();

                String sqlcmd = "XRAI_SELECT_VERIFY_ACCESS";  

                SqlParameter[] paramCollection = new SqlParameter[2];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@UserId";
                paramCollection[0].Value = userid;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@Password";
                paramCollection[1].Value = password;

            //    result = base.ExecuteScalar(sqlcmd, paramCollection, ConnectionString);



            }
            catch (Exception ex)
            {

            }
            //  result.Add(_resultitem);

            return result == "Y";
        }

        public System.Data.DataSet SelectUserRole(String userId)
        {
            //  String ConnectionString = ApplicationCommonController.GetASDConnectionString();
            String ConnectionString = ApplicationCommon.GetAiesDbConnectionString();
            const string sqlCommand = StoredProcedures.ProcedureName;
            DataSet dsDatabase = new DataSet();
            DataTable dt = new DataTable();
            SqlParameter[] paramCollection = new SqlParameter[1];

            paramCollection[0] = new SqlParameter();
            paramCollection[0].SqlDbType = SqlDbType.VarChar;
            paramCollection[0].ParameterName = "@USER_ID";
            paramCollection[0].Value = userId;

        //    dsDatabase = base.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, ConnectionString);

            return dsDatabase;
        }


    }
}