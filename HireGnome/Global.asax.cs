using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HireGnome.App_Start;
using System.Data.Entity;

namespace HireGnome
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //Database.SetInitializer<MainDbContext>(null); // Doesn't do anything
            //Database.SetInitializer<MainDbContext>(new MigrateDatabaseToLatestVersion<MainDbContext, HireGnome.Migrations.Configuration>()); // Restart DB Migrations 
            //Database.SetInitializer<MainDbContext>(new DropCreateDatabaseIfModelChanges<MainDbContext>());
            //Database.SetInitializer<MainDbContext>(new DropCreateDatabaseAlways<MainDbContext>());
            //Database.SetInitializer<MainDbContext>(new );
        }


        public static void RegisterRoutes(RouteCollection routes)
        {
            // routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // https://msdn.microsoft.com/en-us/library/cc668201.aspx#adding_routes_to_an_mvc_application
            routes.MapRoute(
                "Home",                                              // Route name 
                "{Kurs1}/{Home}",                           // URL with parameters 
                new { controller = "Kurs1", action = "Home"}  // Parameter defaults
            );
            routes.MapRoute(
                "Gallery",                                              // Route name 
                "{Kurs1}/{Gallery}",                           // URL with parameters 
                new { controller = "Kurs1", action = "Gallery" }  // Parameter defaults
            );
            routes.MapRoute(
                "Home",                                              // Route name 
                "{Kurs1}/{Contact}",                           // URL with parameters 
                new { controller = "Kurs1", action = "Contact" }  // Parameter defaults
            );

        }
    }
}
