using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASD.DataLayer;
using Logger;
using ASD.BW.Common;

namespace ASD.BW.Models.Modelling
{
    public class Transformtion
    {

        DataAccess objDA = new DataAccess();
        ErrorLog errlog = new ErrorLog();


        public DataTable GetTransformationData(int PageNo, int PageSize, string TableName, string RowField, string SelectedFields,
                                                string WhereCondition, string ClientID, string ProjectID, string SystemID, string selectSecrchOption
                                            )
        {
            DataSet dsTransformationData = new DataSet();
            DataTable dtTransformationData = new DataTable();

            try
            {
                String connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SPSelectTransformationDetails;

                SqlParameter[] paramCollection = new SqlParameter[10];


                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.Int;
                paramCollection[0].ParameterName = "@PageNo";
                paramCollection[0].Value = PageNo;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.Int;
                paramCollection[1].ParameterName = "@PageSize";
                paramCollection[1].Value = PageSize;

                paramCollection[2] = new SqlParameter();
                paramCollection[2].SqlDbType = SqlDbType.VarChar;
                paramCollection[2].ParameterName = "@TableName";
                paramCollection[2].Value = TableName;

                paramCollection[3] = new SqlParameter();
                paramCollection[3].SqlDbType = SqlDbType.VarChar;
                paramCollection[3].ParameterName = "@RowField";
                paramCollection[3].Value = RowField;

                paramCollection[4] = new SqlParameter();
                paramCollection[4].SqlDbType = SqlDbType.VarChar;
                paramCollection[4].ParameterName = "@SelectedFields";
                paramCollection[4].Value = SelectedFields;

                paramCollection[5] = new SqlParameter();
                paramCollection[5].SqlDbType = SqlDbType.VarChar;
                paramCollection[5].ParameterName = "@WhereCondition";
                paramCollection[5].Value = WhereCondition;

                paramCollection[6] = new SqlParameter();
                paramCollection[6].SqlDbType = SqlDbType.VarChar;
                paramCollection[6].ParameterName = "@ClientID";
                paramCollection[6].Value = ClientID;

                paramCollection[7] = new SqlParameter();
                paramCollection[7].SqlDbType = SqlDbType.VarChar;
                paramCollection[7].ParameterName = "@ProjectID";
                paramCollection[7].Value = ProjectID;

                paramCollection[8] = new SqlParameter();
                paramCollection[8].SqlDbType = SqlDbType.VarChar;
                paramCollection[8].ParameterName = "@SystemID";
                paramCollection[8].Value = SystemID;

                paramCollection[9] = new SqlParameter();
                paramCollection[9].SqlDbType = SqlDbType.VarChar;
                paramCollection[9].ParameterName = "@selectSecrchOption";
                paramCollection[9].Value = selectSecrchOption;


                dsTransformationData = objDA.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsTransformationData.Tables.Count > 0)
                {
                    dtTransformationData = dsTransformationData.Tables[0];
                }

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "GetSolutionOverviewData");
            }
            return dtTransformationData;


        }

    }
}
