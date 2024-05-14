using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ogani.Startup))]
namespace Ogani
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
