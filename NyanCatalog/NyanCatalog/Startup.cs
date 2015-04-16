using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NyanCatalog.Startup))]
namespace NyanCatalog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
