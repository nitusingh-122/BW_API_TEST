using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using ASD.BW.SolutionOverview;
using Logger;


namespace ASD.ApplicationService.Controllers.BW.BW_Report
{
    [RoutePrefix("api/bw/v1/overview")]
    public class OverviewController : ApiController
    {
        ErrorLog errlog = new ErrorLog();


        //api/bw/v1/overview/solutionoverview/bpepv01wfw
        // [Authorize]
        [Route("solutionoverview/{projectid}")]
        public HttpResponseMessage GetSolutionOverviewData(string projectid)
        {
            DataTable dtnavigationtreeDetails = new DataTable();
            try
            {
                SolutionOverview bw = new SolutionOverview();
                dtnavigationtreeDetails = bw.GetSolutionOverviewData(projectid);
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetSolutionOverviewData");
            }

            return Request.CreateResponse(HttpStatusCode.OK, dtnavigationtreeDetails);

        }

       
    }
}
