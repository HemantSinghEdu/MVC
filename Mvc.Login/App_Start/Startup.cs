using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Mvc.Login.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var cookieAuthOpt = new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            };
            app.UseCookieAuthentication(cookieAuthOpt);
        }
    }
}