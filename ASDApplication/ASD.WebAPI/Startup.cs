using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ASD.WebAPI.Startup))]

namespace ASD.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
