using ASD.ApplicationService.Controllers;
using ASD.ApplicationService.Models.DataAccess;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASD.ApplicationService
{
    public class AuthenticationProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // 

            // String uservalue = GetAuthenticateUser();
            // context.SetError("UnknownUser", uservalue);

            // ApplicationCommon.WriteLog("UserInfo", "UserID  >  " + uservalue, "Bot", 1);

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            String usrRole = "User";
            bool flag = false;
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            DataRepository dr = new DataRepository();
            // String _authenticateUser = ApplicationCommon.GetAuthenticatedUser();
            //bool flag = dr.VerifyAccess(_authenticateUser, context.Password);
             flag = dr.VerifyAccess(context.UserName, context.Password);
            
            //flag="admin","user",'deployment',"noacces"

            if (flag)
            {
                usrRole = "User";
            }
            else if (!flag)
            {

                //flag = dr.VerifyCustomAccess(context.UserName, context.Password);
                usrRole = "ExtUser";


            }
        




            UserController uc = new UserController();

            //    System.Data.DataSet ds = uc.GetUserRole(context.UserName);
            // if (ds.Tables[0].Rows.Count > 0)
            //  usrRole = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            usrRole = "User";

            if (context.UserName == "admin" && context.Password == "Admin@0123")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Admin User "));
                context.Validated(identity);
            }

            //else if (flag)
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, usrRole));
            //    identity.AddClaim(new Claim("username", context.UserName));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "Application User"));
            //    context.Validated(identity);

            //}

            //else
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, "USER"));
            //    identity.AddClaim(new Claim("username", context.UserName));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "Application User"));
            //    context.Validated(identity);
            //}

            else
            {
                context.SetError("Invalid User");
            }
         
        }

 
    }
}