using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Loja.Mvc.Startup))]
namespace Loja.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
