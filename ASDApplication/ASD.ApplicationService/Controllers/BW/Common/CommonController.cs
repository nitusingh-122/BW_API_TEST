using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using ASD.BW;
using Logger;

namespace ASD.ApplicationService.Controllers.Common
{
    [RoutePrefix("api/bw/v1/common")]
    public class CommonController : ApiController
    {
        ErrorLog errlog = new ErrorLog();


        //api/bw/v1/common/users/s.j.subramaniam
        [Route("users/{userId}")]
        [HttpPost]
        public IHttpActionResult GetUserId(string userId)
        {
            ASD.BW.Common.Common bw = new ASD.BW.Common.Common();
            bool result = bw.GetUserId(userId);
            return Ok(result);
        }


        //api/bw/v1/common/clients/s.j.subramaniam/sapbw
        //[Authorize]
        [HttpGet]
        [Route("clients/{userId}/{appType}")]
        public HttpResponseMessage GetClientsDetailsByUserId(string userId, string appType)
        {
            DataTable dtProjectDetails = new DataTable();
            try
            {
                ASD.BW.Common.Common bw = new ASD.BW.Common.Common();
                dtProjectDetails = bw.GetClientDetailsByUserId(userId, appType);
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetClientsDetailsByUserId");
            }
            return Request.CreateResponse(HttpStatusCode.OK, dtProjectDetails);
        }


        //api/bw/v1/common/projects/s.j.subramaniam/bpe/sapbw
        // [Authorize]
        [HttpGet]
        [Route("projects/{userId}/{clientId}/{appType}")]
        public HttpResponseMessage GetProjectDetailsByUserId(string userId, string clientId, string appType)
        {
            DataTable dtprojectDetails = new DataTable();
            try
            {
                ASD.BW.Common.Common bw = new ASD.BW.Common.Common();
                dtprojectDetails = bw.GetProjectDetailsByUserId(userId, clientId, appType);
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetProjectDetailsByUserId");
            }
            return Request.CreateResponse(HttpStatusCode.OK, dtprojectDetails);

        }

        //api/bw/v1/common/navigationtree/bpepv01wfw
        // [Authorize]
        [HttpGet]
        [Route("navigationtree/{projectid}")]
        public HttpResponseMessage GetNavigationTreeByProjectId(string projectid)
        {
            string result = "";
            try
            {
                ASD.BW.Common.Common bw = new ASD.BW.Common.Common();
                result = bw.GetNavigationTreeByProjectId(projectid);
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetNavigationTreeByProjectId");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        //api/bw/v1/common/tq/gn/bpepv01wfw
        //api/bw/v1/common/tq/fq/bpepv01wfw
        // [Authorize]
        [Route("tq/{sapfmid}/{projectid}")]
        public HttpResponseMessage GetTransitionQuestions(string sapfmid, string projectid)
        {
            DataTable dtTQData = new DataTable();
            try
            {
                ASD.BW.Common.Common bw = new ASD.BW.Common.Common();
                dtTQData = bw.GetTransitionQuestions(sapfmid, projectid);
            }
            catch (Exception ex)
            {
                errlog.Log("BW", "Error", ex.Message, "ApiControllerClass", "GetTransitionQuestions");
            }

            return Request.CreateResponse(HttpStatusCode.OK, dtTQData);

        }


    }
}
