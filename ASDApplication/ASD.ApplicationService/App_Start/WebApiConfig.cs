using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Net.Http.Formatting;


namespace ASD.ApplicationService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        { 
          // Web API configuration and services
          // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //To produce JSON format add this line of code
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            //EnableCorsAttribute cors = new EnableCorsAttribute("https://asdsap.accenture.com", "*", "*");
            //EnableCorsAttribute cors = new EnableCorsAttribute("https://asdsapqa.accenture.com", "*", "*");  
            //config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
             name: "DefaultApi2",
             routeTemplate: "api/{controller}/{action}/{id}",
             defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
           name: "DefaultApi3",
           routeTemplate: "api/bw/{controller}/v1/{action}/{id}",
           defaults: new { id = RouteParameter.Optional });

        }

        
    }
}
