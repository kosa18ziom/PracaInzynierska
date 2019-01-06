using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Praca_Inżynierska.Startup))]
namespace Praca_Inżynierska
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
