using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectAlumni.Startup))]
namespace ProjectAlumni
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
