using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NaccNigModels.Startup))]
namespace NaccNigModels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
