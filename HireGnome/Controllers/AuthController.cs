using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HireGnome.Models;
using System.Security.Claims; // Credential validation
using HireGnome.CustomLibraries; // De/Encrypt

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

            using(var db = new MainDbContext())
            {
                var emailCheck = db.Users.FirstOrDefault(u => u.Email == model.Email);
                var getPassword = db.Users.Where(u => u.Email == model.Email).Select(u => u.Password);

                var materializePassword = getPassword.ToList();
                var password = materializePassword[0];
                var decryptedPassword = CustomDecrypt.Decrypt(password);


                if(model.Email != null && model.Password == decryptedPassword)
                {
                    var getName = db.Users.Where(u => u.Email == model.Email).Select(u => u.Name);
                    var materializeName = getName.ToList();
                    var name = materializeName[0];

                    var getCountry = db.Users.Where(u => u.Email == model.Email).Select(u => u.Country);
                    var materializeCountry = getCountry.ToList();
                    var country = materializeCountry[0];

                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.Country, country),
                        new Claim(ClaimTypes.Email, model.Email) // Or materialize email ...
                    }, "ApplicationCookie");


                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
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
                    var encryptedPassword = CustomEncrypt.Encrypt(model.Password);
                    var user = db.Users.Create();

                    user.Email = model.Email;
                    user.Password = encryptedPassword;
                    user.Country = model.Country;
                    user.Name = model.Name;

                    db.Users.Add(user);
                    db.SaveChanges();
                }

                return RedirectToAction("Login", "Auth");
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }

            return View();
        }
       
    }
}