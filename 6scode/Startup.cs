using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_6scode.Startup))]
namespace _6scode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
