using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NaccNig.Startup))]
namespace NaccNig
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
