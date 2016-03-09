using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebOlx2.Startup))]
namespace WebOlx2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
