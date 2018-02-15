using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Login.Mvc.Startup))]
namespace Login.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
