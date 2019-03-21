using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web21032019.Startup))]
namespace Web21032019
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
