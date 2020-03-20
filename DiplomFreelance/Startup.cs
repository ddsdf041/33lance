using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiplomFreelance.Startup))]
namespace DiplomFreelance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
