using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HireGnome.Models;
using System.Security.Claims;
using System.Data.Entity;

namespace HireGnome.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var db = new MainDbContext();
            return View(db.Products.ToList().Where(u => u.IsPublic == true));
        }

        [HttpGet]
        public ActionResult Show(int gnome_id)
        {
            using (var db = new MainDbContext())
            {
                var model = db.Products.Find(gnome_id);
                if (model == null || !model.IsPublic)
                    return HttpNotFound();
                return View(model);
            }
        }
    }
}