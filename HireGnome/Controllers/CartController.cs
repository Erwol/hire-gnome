using HireGnome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HireGnome.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        // GET: Cart; Show the content of the cart
        public ActionResult Index()
        {
            var empty = Enumerable.Empty<Carts>();
            return View(empty);
        }
    }
}