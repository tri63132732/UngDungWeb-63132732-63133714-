using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocTruyen.Startup))]
namespace DocTruyen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
