using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_Alumni.Startup))]
namespace Project_Alumni
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
