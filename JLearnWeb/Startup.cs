using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JLearnWeb.Startup))]
namespace JLearnWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
