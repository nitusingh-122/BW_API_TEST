using System;
using System.Configuration;

namespace ASD.BW
{
    public class ApplicationCommon
    {

        public static String GetConnectionString(String dbtype)
        {
            String connectionstring = String.Empty;
            switch (dbtype)
             {
                case "AIES":
                  
                    connectionstring = ConfigurationManager.ConnectionStrings["AIDBConnectionString"].ToString();

                    break;
                case "BW":
                    connectionstring = ConfigurationManager.ConnectionStrings["BWConnectionString"].ToString();
                    
                    break;
                default:
                    connectionstring = ConfigurationManager.AppSettings["AIDBConnectionString"].ToString();
                    break;
            }

            return connectionstring;
        }



    }
}
