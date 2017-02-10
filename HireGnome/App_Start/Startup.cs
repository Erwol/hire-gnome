using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

// WWW
using System.Web.Services.Description;

namespace HireGnome.App_Start
{
    public class Startup
    {
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
                
            });
        }

        /*
         * 
         * I Also tried with the Global.asax file...
        public void ConfigureServices(ServiceCollection services)
        {

            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Kurs1}/{action=Home}");
                routes.MapRoute(
                   name: "gallery",
                   template: "{controller=Kurs1}/{action=Gallery}");
                routes.MapRoute(
                   name: "contact",
                   template: "{controller=Kurs1}/{action=Contact}");
            });
        }

    */
    }
}

