using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ASD.DataLayer;

namespace Logger
{
    public class ErrorLog 
    {
        

        public String Log(string application, string type, string message,string methodType, string methodName)
        {
            DataAccess objDA = new DataAccess();
            String AppFlag = "Success";
            if (type == "Error")
                AppFlag = "Error";
            else
                AppFlag = "Success";

            try
            {
                string sqlCommand = "XRAPP_INSERT_ERRORLOG";
                String connectionString = GetBWConnectionString();
                SqlParameter[] paramCollection = new SqlParameter[5];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@AppType";
                paramCollection[0].Value = application;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@MessageType";
                paramCollection[1].Value = type;

                paramCollection[2] = new SqlParameter();
                paramCollection[2].SqlDbType = SqlDbType.VarChar;
                paramCollection[2].ParameterName = "@ERRORMESSAGE";
                paramCollection[2].Value = message;

                paramCollection[3] = new SqlParameter();
                paramCollection[3].SqlDbType = SqlDbType.VarChar;
                paramCollection[3].ParameterName = "@MethodType";
                paramCollection[3].Value = methodType;

                paramCollection[4] = new SqlParameter();
                paramCollection[4].SqlDbType = SqlDbType.VarChar;
                paramCollection[4].ParameterName = "@MethodName";
                paramCollection[4].Value = methodName;


                objDA.ExecuteNonQuery(sqlCommand, paramCollection, CommandType.StoredProcedure, connectionString);


            }
            catch (Exception ex)
            {
                
            }

            return "Success";

        }

        public static String GetBWConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AIDBConnectionString"].ToString();
        }

    }
}
