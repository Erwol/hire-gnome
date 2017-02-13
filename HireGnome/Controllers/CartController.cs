using HireGnome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using HireGnome.ViewModels;

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
            using (var db = new MainDbContext())
            {
                var carts = db.Carts.ToList().Where(x => x.User.Name == User.Identity.Name);
                if (carts == null)
                    return HttpNotFound();
                return View(carts);
            }

        }

        // Displays the products inside the main cart
        public ActionResult CartContent()
        {
            using(var db = new MainDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                var main_cart = db.Carts.FirstOrDefault(x => x.User.Id == user.Id && x.IsMainCart == true);
                var cart_model = new ShowCartViewModel();

                // Fill the cart_model
                cart_model.Name = main_cart.Name;
                cart_model.Id = main_cart.Id;
                cart_model.UserId = main_cart.User.Id;
                cart_model.Products = new List<string>();
                for(int i = 0; i < main_cart.Products.Count; i++)
                {
                    var gnome = main_cart.Products.ElementAt(i);
                    double price = 0;
                    if(gnome.Offer > 0)
                    {
                        price = gnome.Price * ((double)gnome.Offer / 100.0);
                        cart_model.Products.Add(gnome.Name + ": " + price + "$ (Price without offer: " + gnome.Price + "$)");
                    }
                    else
                    {
                        cart_model.Products.Add(gnome.Name + ". " + gnome.Price + "$");
                    }
                        
                }
                    
                return View(cart_model);
            }
            
        }

        // Adds the product id in the main cart and returns a JSon with all the products
        [HttpGet]
        public ActionResult Add(int Id)
        {
            using (var db = new MainDbContext())
            {
                var product = db.Products.Find(Id);
                if (product == null)
                    return HttpNotFound();

                var user = db.Users.Where(x => x.Name == User.Identity.Name).FirstOrDefault();

                var cart = db.Carts.Where(x => x.User.Id == user.Id && x.IsMainCart == true).FirstOrDefault();
                if (cart == null)
                    return HttpNotFound();
                cart.Products.Add(product); 
                db.SaveChanges();

                return RedirectToAction("CartContent");

            }
        }

        public void changeMainCart()
        {

        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            using (var db = new MainDbContext())
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

/*
namespace HireGnome.Controllers
{
    // TODO Allow Anonymous shopping carts
    //[Authorize(Roles = "admin, user")]
    [AllowAnonymous]
    public class CartController : Controller
    {
        // GET: Cart; Show the content of the cart
        public ActionResult Index()
        {
            var empty = Enumerable.Empty<Carts>();
            return View(empty);
        }

        // Returns a View with all the Carts data
        [Authorize(Roles="admin, user")]
        public ActionResult ListCarts()
        {
            using(var db = new MainDbContext())
            {
                var carts = db.Carts.ToList().Where(x => x.User.Name == User.Identity.Name);
                if (carts == null)
                    return HttpNotFound();
                return View(carts);
            } 
        }

        // Return a Json with all the user carts
        [Authorize(Roles = "admin, user")]
        public ActionResult GetCarts()
        {
            using (var db = new MainDbContext())
            {
                var carts = db.Carts.ToList().Where(x => x.User.Name == User.Identity.Name);
                if (carts == null)
                    return HttpNotFound();
                return Json(carts, JsonRequestBehavior.AllowGet);
            }
        }

        // Adds the product id in the main cart
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Add(int Id)
        {
            using(var db = new MainDbContext())
            {
                var product = db.Products.Find(Id);
                if (product == null)
                    return HttpNotFound();

                var anom_user = db.AnomIdentities.Create();
                var user = db.Users.Where(x => x.Name == User.Identity.Name).FirstOrDefault();

                    var cart = db.Carts.Where(x => x.User.Id == user.Id && x.IsMainCart == true).FirstOrDefault();
                    if (cart == null)
                        return HttpNotFound();
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
}*/
