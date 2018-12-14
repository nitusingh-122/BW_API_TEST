using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Linq;
using System.Text;
using Logger;
using ASD.ApplicationService.Models.Classes;
using ASD.BW.Models.Classes;
using Newtonsoft.Json.Linq;

namespace ASD.ApplicationService.Controllers.BW.BW_Report.Modelling
{
    [RoutePrefix("api/bw/v1/modelling")]
    public class ModellingController : ApiController
    {
        ErrorLog errlog = new ErrorLog();

        //api/bw/v1/modelling/BW_MO_KFIO/bpepv01wfw
        //api/bw/v1/modelling/BW_MO_CIO/bpepv01wfw 
        //[Authorize]
        [Route("{reportid}/{projectid}")]
        public virtual HttpResponseMessage GetModellingReportData(string reportid, string projectid)
        {
            DataSet dsModellingReportData = new DataSet();
            try
            {

                ASD.BW.Models.Modelling.Modelling bw = new ASD.BW.Models.Modelling.Modelling();
                dsModellingReportData = bw.GetModellingReportData(reportid, projectid);
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetModellingReportData");
            }
            return Request.CreateResponse(HttpStatusCode.OK, dsModellingReportData);

        }

        //api/bw/v1/modelling/ciodetails/0TCTDTPID/NULL/NULL/bpepv01wfw
        //api/bw/v1/modelling/ciodetails/NULL/0TCTDTPID/NULL/bpepv01wfw
        //api/bw/v1/modelling/ciodetails/NULL/NULL/DCW82EH56X6IE17QMCPP9L361/bpepv01wfw 
        //[Authorize]
        [Route("ciodetails/{cioinfoobject}/{cioreportid}/{ciotransferid}/{projectid}")]
        public HttpResponseMessage GetCioDetailsData(string cioinfoobject, string cioreportid,  string ciotransferid, string projectid)
        {
            DataTable dtCIODetailsData = new DataTable();
            try
            {
                ASD.BW.Models.Modelling.Modelling bw = new ASD.BW.Models.Modelling.Modelling();
                dtCIODetailsData = bw.GetCioDetailsData(cioinfoobject, cioreportid, ciotransferid, projectid);
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetCioDetailsData");
            }

            return Request.CreateResponse(HttpStatusCode.OK, dtCIODetailsData);

        }

        //api/bw/v1/modelling/cioanalysis/bpepv01wfw
        //[Authorize]
        //[Route("cioanalysis/{projectid}")]
        //public HttpResponseMessage GetCioAnalysisData(string projectid)
        //{
        //    ResponseModel objResponseModel = new ResponseModel();
        //    DataSet dsCioAnalysisData = new DataSet();
        //    try
        //    {
        //        ASD.BW.Models.Modelling.Modelling bw = new ASD.BW.Models.Modelling.Modelling();
        //        dsCioAnalysisData = bw.GetCioAnalysisData(projectid);
        //    }
        //    catch (Exception ex)
        //    {
        //        errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetCioAnalysisData");
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, dsCioAnalysisData); 

        //}

        //api/bw/v1/modelling/cioanalysis/bpepv01wfw

        [Route("cioanalysis/{projectid}")]
        public HttpResponseMessage GetCioAnalysisData(string projectId)
        {
            var lstCioAnalysisData = new List<List<CIOAnalysis>>();
            try
            {
                ASD.BW.Models.Modelling.Modelling bw = new ASD.BW.Models.Modelling.Modelling();
                lstCioAnalysisData = bw.GetCioAnalysisData(projectId);
                if (!lstCioAnalysisData.Any())
                {
                    string message = string.Format("No Record found with ID = {0}", projectId);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                }
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetCioAnalysisData");
            }
            return Request.CreateResponse(HttpStatusCode.OK, lstCioAnalysisData);

        }


        //api/bw/v1/modelling/kfioanalysis/bpepv01wfw
        //[Authorize]
        [Route("kfioanalysis/{projectid}")]
        public HttpResponseMessage GetKeyFigureAnalysisData(string projectId)
        {
            var lstKfioAnalysisData = new List<KFIOAnalysis>();
            try
            {
                ASD.BW.Models.Modelling.Modelling bw = new ASD.BW.Models.Modelling.Modelling();
                lstKfioAnalysisData = bw.GetKeyFigureAnalysisData(projectId);
                if (!lstKfioAnalysisData.Any())
                {
                    string message = string.Format("No Record found with ID = {0}", projectId);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                }
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetKeyFigureAnalysisData");
            }

            return Request.CreateResponse(HttpStatusCode.OK, lstKfioAnalysisData);

        }


        //api/bw/v1/modelling/infosetdetails/GRBSI010/NULL/NULL/bpepv01wfw
        //api/bw/v1/modelling/infosetdetails/NULL/GRBSI010/NULL/bpepv01wfw
        //api/bw/v1/modelling/infosetdetails/NULL/NULL/GRBSI010/bpepv01wfw 
        //[Authorize]
        [Route("infosetdetails/{isInfocube}/{isInfoobject}/{isDso}/{projectid}")]
        public HttpResponseMessage GetInfosetDetails(string isInfocube, string isInfoobject, string isDso, string projectid)
        {
            DataTable dtInfosetDetails = new DataTable();
            try
            {
                ASD.BW.Models.Modelling.Modelling bw = new ASD.BW.Models.Modelling.Modelling();
                dtInfosetDetails = bw.GetInfosetDetails(isInfocube, isInfoobject, isDso, projectid);
            }
            catch (Exception ex)
            { 
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetCioDetailsData");
            }

            return Request.CreateResponse(HttpStatusCode.OK, dtInfosetDetails);

        }

        //api/bw/v1/modelling/infosetanalysis/bpepv01wfw
        //[Authorize]
        [Route("infosetanalysis/{projectid}")]
        public HttpResponseMessage GetInfosetAnalysis(string projectId)
        {
            var lstInfosetAnalysisData = new List<List<InfosetAnalysis>>();
            try
            {
                ASD.BW.Models.Modelling.Modelling bw = new ASD.BW.Models.Modelling.Modelling();
                lstInfosetAnalysisData = bw.GetInfosetAnalysis(projectId);
                if (!lstInfosetAnalysisData.Any())
                {
                    string message = string.Format("No Record found with ID = {0}", projectId);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                }
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetInfosetAnalysis");
            }

            return Request.CreateResponse(HttpStatusCode.OK, lstInfosetAnalysisData);

        }


    }
}