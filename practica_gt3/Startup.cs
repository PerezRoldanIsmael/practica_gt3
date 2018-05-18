using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(practica_gt3.Startup))]
namespace practica_gt3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
