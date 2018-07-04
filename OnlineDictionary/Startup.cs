using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineDictionary.Startup))]
namespace OnlineDictionary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
