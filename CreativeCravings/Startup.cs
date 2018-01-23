using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreativeCravings.Startup))]
namespace CreativeCravings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
