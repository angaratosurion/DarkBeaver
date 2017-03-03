using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DarkBeaver.Startup))]
namespace DarkBeaver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
