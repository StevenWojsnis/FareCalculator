using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Guggenheim.Startup))]
namespace Guggenheim
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
