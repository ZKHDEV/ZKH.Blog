using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZKH.Blog.UI.Portal.Startup))]
namespace ZKH.Blog.UI.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
