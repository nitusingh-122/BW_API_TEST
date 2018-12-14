using ASD.ApplicationService.Models.Common;
using ASD.ApplicationService.Models.DataAccess;
using Logger;
using System;
using System.Web.Http;

namespace ASD.ApplicationService.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {

        ErrorLog dblog = new ErrorLog();

        [AllowAnonymous]
        [HttpGet]
        [Route("api/login/v1/User/LoggedUser")]
        public string GET()
        {

            try
            {
                //userDetails = userBL.GetAuthenticatedUser();
                String _authenticateUser = ApplicationCommon.GetAuthenticatedUser();
                return _authenticateUser;
            }
            catch (Exception ex)
            {
                return dblog.Log("BW", "ERROR", ex.Message, "ApiControllerClass", "GET");
            }


        }


        public System.Data.DataSet GetUserRole(String userId)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            DataRepository rep = new DataRepository();
            try
            {
                ds = rep.SelectUserRole(userId);

            }
            catch (Exception ex)
            {
                dblog.Log("BW", "ERROR", ex.Message, "ApiControllerClass", "GetUserRole");
            }
            return ds;
        }











    }
}
