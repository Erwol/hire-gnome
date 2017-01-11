using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HireGnome.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // Right click directly in the method's name to create views!
        public ActionResult Index()
        {
            return View();
        }
    }
}