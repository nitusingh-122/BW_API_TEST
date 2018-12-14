using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace ASD.ApplicationService.Models.Common
{
    public class ApplicationCommon
    {

        #region Constants values

        public const String ValueSelect = "--Select--";
        public const String ValueAll = "ALL";
        public const String MasterDb = "AIES";
        public const String TypeApplication = "Application";
        public const String TypeClient = "Client";
        public const String TypeProject = "Project";
        public const String ExtBak = ".BAK";
        public const String ExtZip = ".ZIP";
        public const String ExtRAR = ".RAR";

        #endregion
        #region Common
        /// <summary>
        /// GetAuthenticatedUser
        /// </summary>
        /// <returns></returns>
        public static String GetAuthenticatedUser()
        {
            HttpContextWrapper hc = new HttpContextWrapper(HttpContext.Current);
            String userId = String.Empty;
            //var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            //userId = HttpContext.Current.User.Identity.Name;
            try
            {

                //    if (claimsIdentity != null)
                //    {

                //        userId = claimsIdentity.Name;
                //    }               

                if (System.Web.HttpContext.Current != null &&
                    System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    //System.Web.Security.MembershipUser usr = Membership.GetUser();
                    //if (usr != null)
                    //{
                    userId = System.Web.HttpContext.Current.User.Identity.Name;
                    //}
                }

                //userId =  hc.User.Identity.Name.Replace(@"DIR\", "")
                //        .Replace(@"GDNINDIA\", "")
                //        .Replace(@"DS\", "")
                //        .Replace("@accenture.com", "");

                userId = System.Web.HttpContext.Current.User.Identity.Name;
                Console.Write("LoggedUser:>" + userId);

                if (userId == null || userId == "")
                {
                    // Please remove this condition while moving to production.  This is only for testing in QA
                    userId = "rajesh.p.menon";
                
                }



            }
            catch (Exception ex)
            {
                //  CommonMethods cm = new CommonMethods();
                // cm.WriteLog("ERROR", "Error Getting Authenticated User " + ex.Message);
                userId = "User : " + ex.Message;

            }


            return userId;
        }

        /// <summary>
        /// GetDbConnectionString
        /// </summary>
        /// <returns></returns>
        public static String GetAiesDbConnectionString()
        {
            string connestring = ConfigurationManager.ConnectionStrings["AIDBConnectionString"].ToString();
            return connestring;
        }
 
        /// <summary>
        /// GetDbConnectionString
        /// </summary>
        /// <returns></returns>
        public static String GetBWConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BWConnectionString"].ToString();
        }

        /// <summary>
        /// GetDbConnectionStringTemplate
        /// </summary>
        /// <returns></returns>
        public static String GetDbConnectionStringTemplate()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionStringTemplate"].ToString();
        }
 
      
        /// <summary>
        /// Class Stored Procedures
        /// </summary>
        /// 

        public static String CreateDirectory(String folderPath, String folderName)
        {
            folderPath = FormatFolder(folderPath);
            String newFolder = String.Format("{0}{1}", folderPath, folderName);
            if (!Directory.Exists(String.Format("{0}{1}", folderPath, folderName)))
                Directory.CreateDirectory(newFolder);
            return newFolder;
        }


        public static String FormatFolder(String folderName)
        {
            int folderlengh = folderName.Length;
            String slashChar = folderName.Substring(folderlengh - 1, 1);
            if (slashChar == "/" || slashChar == "\\")
                return folderName;
            else
                return string.Concat(folderName, @"\");
        }



        #endregion Common



 


    }
}