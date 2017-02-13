using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HireGnome.Controllers
{
    [AllowAnonymous]
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confirmation()
        {
            using (var db = new MainDbContext())
            {
                var model = db.Bills.Create();

                // Searching the logged in user and taking his main cart
                var user = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                var main_cart = db.Carts.FirstOrDefault(x => x.User.Id == user.Id && x.IsMainCart);

                // Deactivating the previous cart; marking it as billed
                main_cart.IsBilled = true;
                main_cart.IsMainCart = false;

                // Creating a new default main cart for this user
                var new_main_cart = db.Carts.Create();
                new_main_cart.User = user;
                new_main_cart.IsMainCart = true;
                return View(model);
            }
        }

        public ActionResult AjaxCall()
        {
            System.Diagnostics.Debug.Write("foo");
            return Content("ajax from the server");
        }

    }
}