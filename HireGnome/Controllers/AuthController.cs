using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HireGnome.Models;
using System.Security.Claims; // Credential validation

namespace HireGnome.Controllers
{
    [AllowAnonymous] // It simply says that everything under your AuthController can be accessed by anyone. This also by-passes the AuthorizeAttribute
    public class AuthController : Controller
    {
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogIn(Users model)
        {
            if (!ModelState.IsValid) //Checks if input fields have the correct format
            {
                return View(model); //Returns the view with the input values so that the user doesn't have to retype again
            }

            if (model.Email == "admin@admin.com" && model.Password == "123456")
            {
                var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, "Ernesto"),
                new Claim(ClaimTypes.Email, "admin@admin.com"),
                new Claim(ClaimTypes.Country, "Spain")
                }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                // return Redirect(GetRedirectUrl(model.ReturnUrl));
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid email or password...");
            return View(model); //Should always be declared on the end of an action method
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Users model)
        {
            if (ModelState.IsValid)
            {
                using(var db = new MainDbContext())
                {

                }
            }

            return View();
        }
       
    }
}