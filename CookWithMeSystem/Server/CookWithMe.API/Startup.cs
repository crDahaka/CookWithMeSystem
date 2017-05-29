using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CookWithMe.API.Startup))]

namespace CookWithMe.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
