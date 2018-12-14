using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ASD.BW;
using ASD.DataLayer;
using Logger;
using ASD.BW.Common;
using ASD.BW.Models.Classes;

namespace ASD.BW.Models.Modelling
{
    public class Modelling
    {
        DataAccess _objDa = new DataAccess();
        ErrorLog errlog = new ErrorLog();


        public DataSet GetModellingReportData(string reportid, string projectid)
        {
            DataSet dsModellingReportData = new DataSet();
            try
            {
                var connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectModellingData;

                SqlParameter[] paramCollection = new SqlParameter[2];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@Report_ID";
                paramCollection[0].Value = reportid;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@ProjectID";
                paramCollection[1].Value = projectid;



                dsModellingReportData = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);


            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetModellingReportData");
            }
            return dsModellingReportData;
        }

        public DataTable GetCioDetailsData(string cioinfoobject, string cioreportid, string ciotransferid, string projectId)
        {
            DataSet dsCioDetailsData = new DataSet();
            DataTable dtCioDetailsData = new DataTable();

            try
            {
                var connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectCioDetails;

                SqlParameter[] paramCollection = new SqlParameter[4];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@CIO_INFO_OBJECT";
                paramCollection[0].Value = cioinfoobject;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@CIO_REPORT_DATA_ID";
                paramCollection[1].Value = cioreportid;

                paramCollection[2] = new SqlParameter();
                paramCollection[2].SqlDbType = SqlDbType.VarChar;
                paramCollection[2].ParameterName = "@CIO_TRANSFER_ROUTINE_ID";
                paramCollection[2].Value = ciotransferid;

                paramCollection[3] = new SqlParameter();
                paramCollection[3].SqlDbType = SqlDbType.VarChar;
                paramCollection[3].ParameterName = "@PROJECT_ID";
                paramCollection[3].Value = projectId;



                dsCioDetailsData = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsCioDetailsData.Tables.Count > 0)
                {
                    dtCioDetailsData = dsCioDetailsData.Tables[0];
                }

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetCioDetailsData");
            }
            return dtCioDetailsData;

        }

        public List<List<CIOAnalysis>> GetCioAnalysisData(string projectid)
        {
            List<List<CIOAnalysis>> lstCioAnalysisData = new List<List<CIOAnalysis>>();
            
            try
            {
                var lstCioAnalysis = new List<CIOAnalysis>();
                DataSet dsCioAnalysisData = new DataSet();
                var connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectCioAnalysis;

                SqlParameter[] paramCollection = new SqlParameter[1];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@PROJECT_ID";
                paramCollection[0].Value = projectid;

                dsCioAnalysisData = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);

                if (dsCioAnalysisData != null && dsCioAnalysisData.Tables.Count > 0)
                {
                    for (int i = 0; i < dsCioAnalysisData.Tables.Count; i++)
                    {
                        lstCioAnalysis = dsCioAnalysisData.Tables[i].AsEnumerable().Select(
                            dataRow => new CIOAnalysis
                            {
                                RowCount = dataRow.Field<int>("Row_Count"),
                                Mode = dataRow.Field<string>("Mode")
                            }).ToList();
                        lstCioAnalysisData.Add(lstCioAnalysis);
                    }
                }
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetCioAnalysisData");
            }
            return lstCioAnalysisData;
        }

        //public DataSet GetCioAnalysisData(string projectid)
        //{
        //    DataSet dsCioAnalysisData = new DataSet();
        //    try
        //    {
        //        var connectionString = ApplicationCommon.GetConnectionString("BW");
        //        const string sqlCommand = StoredProcedures.SpSelectCioAnalysis;

        //        SqlParameter[] paramCollection = new SqlParameter[1];

        //        paramCollection[0] = new SqlParameter();
        //        paramCollection[0].SqlDbType = SqlDbType.VarChar;
        //        paramCollection[0].ParameterName = "@PROJECT_ID";
        //        paramCollection[0].Value = projectid;

        //        dsCioAnalysisData = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);

        //    }
        //    catch (Exception ex)
        //    {
        //        errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetCioAnalysisData");
        //    }
        //    return dsCioAnalysisData;
        //}

        public List<KFIOAnalysis> GetKeyFigureAnalysisData(string projectId)
        {
            List<KFIOAnalysis> lstKfioAnalysisData = new List<KFIOAnalysis>();
            
            try
            {
                //var lstKfioAnalysis = new List<KFIOAnalysis>();
                var lstKfioMode = new List<KFIOMode>();
                var lstKfioException = new List<KFIOException>();


                DataSet dsKfioAnalysisData = new DataSet();
                var connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectKeyFigureAnalysis;
                SqlParameter[] paramCollection = new SqlParameter[1];
                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@PROJECT_ID";
                paramCollection[0].Value = projectId;

                dsKfioAnalysisData = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);

                if (dsKfioAnalysisData != null && dsKfioAnalysisData.Tables.Count > 0)
                {
                    lstKfioMode = dsKfioAnalysisData.Tables[0].AsEnumerable().Select(
                            dataRow => new KFIOMode {
                               RowCount = dataRow.Field<int>("Row_Count"),
                               Mode = dataRow.Field<string>("Mode")
                                   }).ToList();
                    
                    //lstKfioAnalysisData.AddRange(lstKfioMode);


                    //lstKfioException = dsKfioAnalysisData.Tables[1].AsEnumerable().Select(
                    //    dataRow => new KFIOException
                    //    {
                    //        KFExceptionAggregation = dataRow.Field<string>("KF_EXCEPTION_AGGREGATION"),
                    //        MaxCount = dataRow.Field<int>("MAX_COUNT")
                    //    }).ToList();
                    //lstKfioAnalysisData.Add(lstKfioException);
                }
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetKeyFigureAnalysisData");
            }
            return lstKfioAnalysisData;
        }

        public DataSet GetKeyFigureAnalysisData1(string projectid)
        {
            DataSet dsKfioAnalysisData = new DataSet();
            try
            {
                var connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectKeyFigureAnalysis;

                SqlParameter[] paramCollection = new SqlParameter[1];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@PROJECT_ID";
                paramCollection[0].Value = projectid;

                dsKfioAnalysisData = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetKeyFigureAnalysisData");
            }
            return dsKfioAnalysisData;
        }

        
        public DataTable GetInfosetDetails(string isInfocube, string isInfoobject, string isDso, string projectId)
        {
            DataSet dsInfosetDetails = new DataSet();
            DataTable dtInfosetDetails = new DataTable();

            try
            {
                var connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectInfosetDetails;

                SqlParameter[] paramCollection = new SqlParameter[4];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@INFO_CUBE  ";
                paramCollection[0].Value = isInfocube;

                paramCollection[1] = new SqlParameter();
                paramCollection[1].SqlDbType = SqlDbType.VarChar;
                paramCollection[1].ParameterName = "@INFO_OBJECT";
                paramCollection[1].Value = isInfoobject;

                paramCollection[2] = new SqlParameter();
                paramCollection[2].SqlDbType = SqlDbType.VarChar;
                paramCollection[2].ParameterName = "@DSO";
                paramCollection[2].Value = isDso;

                paramCollection[3] = new SqlParameter();
                paramCollection[3].SqlDbType = SqlDbType.VarChar;
                paramCollection[3].ParameterName = "@PROJECT_ID";
                paramCollection[3].Value = projectId;



                dsInfosetDetails = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);
                if (dsInfosetDetails.Tables.Count > 0)
                {
                    dtInfosetDetails = dsInfosetDetails.Tables[0];
                }

            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetInfosetDetails");
            }
            return dtInfosetDetails;

        }

        public List<List<InfosetAnalysis>> GetInfosetAnalysis(string projectid)
        {
            List<List<InfosetAnalysis>> lstInfoAnalysisData = new List<List<InfosetAnalysis>>();

            try
            {
                var lstInfosetAnalysis = new List<InfosetAnalysis>();
                DataSet dsInfoAnalysisData = new DataSet();
                var connectionString = ApplicationCommon.GetConnectionString("BW");
                const string sqlCommand = StoredProcedures.SpSelectInfosetAnalysis;
                SqlParameter[] paramCollection = new SqlParameter[1];
                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@PROJECT_ID";
                paramCollection[0].Value = projectid;

                dsInfoAnalysisData = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);

                if (dsInfoAnalysisData != null && dsInfoAnalysisData.Tables.Count > 0)
                {
                    lstInfosetAnalysis = dsInfoAnalysisData.Tables[0].AsEnumerable().Select(
                        dataRow => new InfosetAnalysis
                        {
                            Infoset = dataRow.Field<string>("INFOSET"),
                            InfosetDesc = dataRow.Field<string>("INFOSET_DESC"),
                            InfosetInfoCube = dataRow.Field<string>("INFOSET_INFO_CUBE"),
                            InfosetInfoObject = dataRow.Field<string>("INFOSET_INFO_OBJECT"),
                            InfosetDSO = dataRow.Field<string>("INFOSET_DSO")
                        }).ToList();
                    lstInfoAnalysisData.Add(lstInfosetAnalysis);

                    lstInfosetAnalysis = dsInfoAnalysisData.Tables[1].AsEnumerable().Select(
                            dataRow => new InfosetAnalysis
                            {
                                RowCount = dataRow.Field<int>("Row_Count"),
                                Mode = dataRow.Field<string>("Mode")
                            }).ToList();
                    lstInfoAnalysisData.Add(lstInfosetAnalysis);
                    
                }
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetInfoAnalysisData");
            }
            return lstInfoAnalysisData;
        }

        //public DataSet GetInfosetAnalysis1(string projectid)
        //{
        //    DataSet dsInfosetAnalysis = new DataSet();
        //    try
        //    {
        //        var connectionString = ApplicationCommon.GetConnectionString("BW");
        //        const string sqlCommand = StoredProcedures.SpSelectInfosetAnalysis;

        //        SqlParameter[] paramCollection = new SqlParameter[1];

        //        paramCollection[0] = new SqlParameter();
        //        paramCollection[0].SqlDbType = SqlDbType.VarChar;
        //        paramCollection[0].ParameterName = "@PROJECT_ID";
        //        paramCollection[0].Value = projectid;

        //        dsInfosetAnalysis = _objDa.ExecuteQuery(sqlCommand, CommandType.StoredProcedure, paramCollection, connectionString);

        //    }
        //    catch (Exception ex)
        //    {
        //        errlog.Log("BW", "Error", ex.Message, "ModelClass", "GetInfosetAnalysis");
        //    }
        //    return dsInfosetAnalysis;
        //}




    }
}
