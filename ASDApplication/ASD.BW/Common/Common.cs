using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.DataLayer;
using System.Web;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using Logger;




namespace ASD.BW.Common
{
    public class Common : ApiController
    {
       
        DataAccess objDA = new DataAccess();
        ErrorLog errlog = new ErrorLog();

        #region

        //private String ConnectionString;

        //public DataSet GetClients(string clientId)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        ds = GetClientList();
        //    }
        //    catch (Exception ex)
        //    {
        //        //  Error Logger
        //    }
        //    return ds;
        //}

        //public DataSet GetClientList()
        //{
        //    ConnectionString = ApplicationCommon.GetConnectionString("BW");

        //    const string sqlCommand = StoredProcedures.SPSelectIndustryName;

        //    DataSet _dataset = new DataSet();
        //    _dataset = objDA.ExecuteQuery(sqlCommand, ConnectionString);

        //    return _dataset;

        //}

        #endregion

        public bool GetUserId(string userId)
        {
            bool flag = false;
            var result = string.Empty;       

            try
            {
                var connectionString = ApplicationCommon.GetConnectionString("AIES");
                var userExists = string.Empty;

                const string sqlCommand = StoredProcedures.SpCheckUser;
                SqlParameter[] paramCollection = new SqlParameter[1];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@USERID";
                paramCollection[0].Value = userId;

                result = objDA.ExecuteScalar(sqlCommand, paramCollection, connectionString);
                flag = result == "Y";
                
            }
            catch (Exception ex)
            {
                flag = false;
                errlog.Log("BW", "ERROR", ex.Message,"ModelClass", "GetUserId");
            }
            return flag;

        }


        public DataTable GetClientDetailsByUserId(string userId, string appType)
        {
            DataSet dsClientDetails = new DataSet();
            DataTable dtClientDetails = new DataTable();

            try
            {
                String connectionString = ApplicationCommon.GetConnectionString("AIES");
                const string sqlCommand = StoredProcedures.SpSelectClientDetails;

                SqlParameter[] paramCollection = new SqlParameter[2];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@userId";
                paramCollection[0].Value = userId;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@appType";
                paramCollection[1].Value = appType;

                dsClientDetails = objDA.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsClientDetails.Tables.Count > 0)
                {
                    dtClientDetails = dsClientDetails.Tables[0];
                }

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "ERROR", ex.Message, "ModelClass", "GetClientDetailsByUserId");
            }
            return dtClientDetails;


        }



        public DataTable GetProjectDetailsByUserId(string userId, string clientId, string appType)
        {
            DataSet dsProjectDetails = new DataSet();
            DataTable dtProjectDetails = new DataTable();
            

            try
            {
                String connectionString = ApplicationCommon.GetConnectionString("AIES");
                const string sqlCommand = StoredProcedures.SpSelectProjectDetails;

                SqlParameter[] paramCollection = new SqlParameter[3];
                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@userId";
                paramCollection[0].Value = userId;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@appType";
                paramCollection[1].Value = appType;

                paramCollection[2] = new SqlParameter();
                paramCollection[2].SqlDbType = SqlDbType.VarChar;
                paramCollection[2].ParameterName = "@ClientId";
                paramCollection[2].Value = clientId;

                dsProjectDetails = objDA.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsProjectDetails.Tables.Count > 0)
                {
                    dtProjectDetails = dsProjectDetails.Tables[0];
                }

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "ERROR", ex.Message, "ModelClass", "GetProjectDetailsByUserId");
            }
            return dtProjectDetails;
        }



        public string GetNavigationTreeByProjectId(string projectid)
        {
            DataSet dsNavigationTreeDetails = new DataSet();
            DataTable dtNavigationTreeDetails = new DataTable();
            DataTable dtNavigationTreeDetails1 = new DataTable();           
            string Result = "";


            try
            {
                String connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectNavigationTreeDetails;

                SqlParameter[] paramCollection = new SqlParameter[1];
                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@ProjectId";
                paramCollection[0].Value = projectid;

                dsNavigationTreeDetails = objDA.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsNavigationTreeDetails.Tables.Count > 0)
                {
                    dtNavigationTreeDetails = dsNavigationTreeDetails.Tables[0];
                    dtNavigationTreeDetails1 = dsNavigationTreeDetails.Tables[1];
                }
                Result = ConvertDataTableTojSonString(dtNavigationTreeDetails, dtNavigationTreeDetails1);

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetNavigationTreeByProjectId");
            }
            return Result;
        }





        public string ConvertDataTableTojSonString(DataTable dt, DataTable dt1)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder jsonStr = new StringBuilder();
            

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                String finalJsonStr = "";
                jsonStr.Append("[");
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int parentRowsCount = dt.Rows.Count;
                    jsonStr.Append("{");
                    int nodeParent = row.Field<int>(0);
                    int nodeId = row.Field<int>(2);
                    var nodelink = row.Field<string>(3);
                    if (nodeParent == 0)
                    {
                        if (nodelink != null)
                        {
                            jsonStr.Append("\"moduleName" + "\":" + "\"" + row.Field<string>(1) + "\"," + "\"nodeLink" + "\":" + "\"" + nodelink + "\",");
                        }
                        else
                        {
                            jsonStr.Append("\"moduleName" + "\":" + "\"" + row.Field<string>(1) + "\"," );
                        }

                        if (dt1.Rows.Count > 0)
                        {
                            jsonStr.Append("\"children" + "\":[");
                        }
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            
                            int childNode = row1.Field<int>(0);
                            var childnodelink = row1.Field<string>(3);
                            if (nodeId == childNode)
                            {
                                jsonStr.Append("{");
                                if (childnodelink != null)
                                {
                                    jsonStr.Append("\"moduleName" + "\":" + "\"" + row1.Field<string>(1) + "\"," + "\"nodeLink" + "\":" + "\"" + childnodelink + "\"");
                                    
                                }
                                else
                                {
                                    jsonStr.Append("\"moduleName" + "\":" + "\"" + row1.Field<string>(1) + "\"");
                                }


                                jsonStr.Append("},");
                            }
                        }

                        jsonStr.Remove(jsonStr.Length - 1, 1);
                        jsonStr.Append("]");
                    }
                    if (i == parentRowsCount - 1)
                    {
                        jsonStr.Append("}");
                    }
                    else
                    {
                        jsonStr.Append("},");
                    }
                    i++;
                }
                jsonStr.Append("]");
                finalJsonStr = jsonStr.ToString().Replace(@"\", string.Empty);
               return finalJsonStr;
            }
            else
            {
                return null;
            }
        }


        public DataTable GetTransitionQuestions(string sapfmid, string projectid)
        {
            DataSet dsTQData = new DataSet();
            DataTable dtTQData = new DataTable();

            try
            {
                String connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectTqData;

                SqlParameter[] paramCollection = new SqlParameter[2];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@SAP_FM_ID";
                paramCollection[0].Value = sapfmid;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@PROJECT_ID";
                paramCollection[1].Value = projectid;

                dsTQData = objDA.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsTQData.Tables.Count > 0)
                {
                    dtTQData = dsTQData.Tables[0];
                }

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetTQData");
            }
            return dtTQData;


        }


    }
}
