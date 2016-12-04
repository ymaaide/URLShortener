using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(URLShortener.Startup))]
namespace URLShortener
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
