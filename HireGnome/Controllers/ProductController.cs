using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HireGnome.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var db = new MainDbContext();
            return View();
        }
    }
}