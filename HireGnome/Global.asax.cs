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
            Database.SetInitializer<MainDbContext>(new DropCreateDatabaseIfModelChanges<MainDbContext>());
            //Database.SetInitializer<MainDbContext>(new DropCreateDatabaseAlways<MainDbContext>());
        }
    }
}
