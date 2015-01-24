using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dossieropvolging.Startup))]
namespace Dossieropvolging
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
