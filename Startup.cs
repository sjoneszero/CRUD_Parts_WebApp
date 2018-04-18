using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aftermarket_WebApp.Startup))]
namespace Aftermarket_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
