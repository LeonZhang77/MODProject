using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wicresoft.BadmintonSystem.Wap.Startup))]
namespace Wicresoft.BadmintonSystem.Wap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
