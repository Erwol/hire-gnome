using HireGnome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using HireGnome.ViewModels;
using HireGnome.Models;

namespace HireGnome.Controllers
{
    // TODO Allow Anonymous shopping carts
    [Authorize(Roles = "admin, user")]
    public class CartController : Controller
    {
        // GET: Cart; Show the content of the cart
        public ActionResult Index()
        {

            using (var db = new MainDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                var user_carts = db.Carts.Where(x => x.User.Id == user.Id);

                ShowUserCartsViewModel model = new ShowUserCartsViewModel();

                model.User = user;
                model.Carts = new List<Carts>();

                foreach (var cart in user_carts)
                    model.Carts.Add(cart);

                return View(model);
            }
                
        }

        // Changes the main cart to the one that contains that ID
        [HttpGet]
        public ActionResult ChangeMainCart(int Id)
        {
            using (var db = new MainDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                // Search the selected cart
                var cart_to_be_changed = db.Carts.FirstOrDefault(x => x.Id == Id);
                if (cart_to_be_changed == null)
                    return HttpNotFound();
                var actual_cart = db.Carts.FirstOrDefault(x => x.User.Id == user.Id && x.IsMainCart == true);

                actual_cart.IsMainCart = false;
                cart_to_be_changed.IsMainCart = true;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
           
        }

        public ActionResult NewCart()
        {
            return View();
        }

        // Create a new cart
        [HttpPost]
        public ActionResult NewCart(Carts cart)
        {
            using(var db = new MainDbContext())
            {
                if (ModelState.IsValid)
                {
                    var user = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);

                    var new_cart = db.Carts.Create();
                    new_cart.Name = cart.Name;
                    new_cart.IsPublic = cart.IsPublic;
                    new_cart.IsMainCart = false;
                    new_cart.ModificationDate = DateTime.Now;
                    new_cart.User = user;
                    user.Carts.Add(new_cart);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", " Something went wrong when adding the cart");
                return View();
            }
        }

        // Displays the products inside the main cart
        public ActionResult CartContent()
        {
            using(var db = new MainDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                var main_cart = db.Carts.FirstOrDefault(x => x.User.Id == user.Id && x.IsMainCart == true);
                return CartContentById(main_cart.Id);
            }
        }

        public ActionResult CartContentById(int Id)
        {
            using (var db = new MainDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                var main_cart = db.Carts.Find(Id);
                var cart_model = new ShowCartViewModel();

                // Fill the cart_model
                cart_model.Name = main_cart.Name;
                cart_model.Id = main_cart.Id;
                cart_model.UserId = main_cart.User.Id;
                cart_model.Products = new List<string>();
                for (int i = 0; i < main_cart.Products.Count; i++)
                {
                    var gnome = main_cart.Products.ElementAt(i);
                    double price = 0;
                    if (gnome.Offer > 0)
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

       
    }
}

