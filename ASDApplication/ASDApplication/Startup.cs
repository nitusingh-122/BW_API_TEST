using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASDApplication.Startup))]
namespace ASDApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
