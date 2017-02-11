using HireGnome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace HireGnome.Controllers
{
    // TODO Allow Anonymous shopping carts
    [Authorize(Roles = "admin, user")]
    public class CartController : Controller
    {
        // GET: Cart; Show the content of the cart
        public ActionResult Index()
        {
            var empty = Enumerable.Empty<Carts>();
            return View(empty);
        }

        public ActionResult MyCarts()
        {
            using(var db = new MainDbContext())
            {
                var carts = db.Carts.ToList().Where(x => x.User.Name == User.Identity.Name);
                if (carts == null)
                    return HttpNotFound();
                return View(carts);
            }
           
        }
        // Adds the product id in the main cart
        [HttpGet]
        public ActionResult Add(int Id)
        {
            using(var db = new MainDbContext())
            {
                var user = db.Users.Find(User);
                var product = db.Products.Find(Id);
                if (product == null)
                    return HttpNotFound();

                var cart = db.Carts.Where(x => x.User == User && x.IsMainCart == true).FirstOrDefault();
                cart.Products.Add(product);
                db.SaveChanges();
                return View(cart);

            }
        }

        public void changeMainCart()
        {

        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            using(var db = new MainDbContext())
            {
                var cart = db.Carts.Find(Id);
                if (cart == null)
                    return HttpNotFound();
                return View(cart);
            }
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(Carts cart)
        {
            if (ModelState.IsValid)
            {

            }
            else
                ModelState.AddModelError("", "Error in cart post request");
            return View();
        }
    }
}