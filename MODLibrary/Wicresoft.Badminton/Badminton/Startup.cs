using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Badminton.Startup))]
namespace Badminton
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
