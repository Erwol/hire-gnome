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

        public ActionResult AjaxCall()
        {
            System.Diagnostics.Debug.Write("foo");
            return Content("ajax from the server");
        }

    }
}