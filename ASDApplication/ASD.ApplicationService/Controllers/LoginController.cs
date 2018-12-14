using ASD.Common.V1.Control;
using ASD.DataLayer;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;

namespace ASD.ApplicationService.Controllers
{
    public class LoginController : ApiController
    {

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        //public IEnumerable<string> Get(AppType appName)
        //{


        //    ApplicationFactory appFactory = new ApplicationFactory();
        //    IApplicationType iType = appFactory.Execute(appName);
        //    AppProperty x = new AppProperty();

        //    iType.Execute(x);
 

        //    return new string[] { "value1", "value2" };
        //}


        //GET bw/v1/users/AIESAdmin
        //GET bw/v1/users/Gayathri.ajai

        [HttpGet]
        [Route("{userId}")]
        public IHttpActionResult CheckIfUserExists(string userId)
        {
            bool flag = false;
            String result = String.Empty;
            DataAccess objDA = new DataAccess(); 

            try
            {
                String connectionString = ApplicationClass.GetDbConnectionString();
                String userExists = String.Empty;

                const string sqlCommand = "XRA_CHECK_USER_EXISTS";
                SqlParameter[] paramCollection = new SqlParameter[1];

                paramCollection[0] = new SqlParameter();
                paramCollection[0].SqlDbType = SqlDbType.VarChar;
                paramCollection[0].ParameterName = "@USERID";
                paramCollection[0].Size = 20;
                paramCollection[0].Value = userId;

                result = objDA.ExecuteScalar(sqlCommand, paramCollection, connectionString);
                flag = result == "Y";
                //var result = new
                //{
                //    x = "hello",
                //    y = "world"
                //};
            }
            catch (Exception ex)
            {
                flag = false;
               // errlog.Log("ASD", "Info", "CheckIfUserExists", userId);
            }
            return Ok(flag);

        }

    }
}
