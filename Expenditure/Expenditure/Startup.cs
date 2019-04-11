using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Expenditure.Startup))]
namespace Expenditure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
