using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
 
[assembly: OwinStartup(typeof(EpamNetProject.PLL.Startup))]

namespace EpamNetProject.PLL
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
