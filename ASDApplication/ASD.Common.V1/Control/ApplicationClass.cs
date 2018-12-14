using System;
using System.Configuration;

namespace ASD.Common.V1.Control
{
    public class ApplicationClass
    {
        public static string connectionstring = string.Empty;

        public static String GetDbConnectionString()
        {
          return ConfigurationManager.ConnectionStrings["AIDBConnectionString"].ToString();
        }

        public static String GetBWConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BWConnectionString"].ToString();
        }
    }

    
}
