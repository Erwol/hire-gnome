using HireGnome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using HireGnome.CustomLibraries;
using HireGnome.ViewModels;

namespace HireGnome.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) //Checks if input fields have the correct format
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model); //Returns the view with the input values so that the user doesn't have to retype again
            }

            using (var db = new MainDbContext())
            {
                var emailCheck = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if(emailCheck == null)
                {
                    ModelState.AddModelError("", model.Email + " is not registered in our database");
                    return View(model); //Returns the view with the input values so that the user doesn't have to retype again
                }
                var getPassword = db.Users.Where(u => u.Email == model.Email).Select(u => u.Password);
                var materializePassword = getPassword.ToList();
                var password = materializePassword[0];
                var decryptedPassword = CustomDecrypt.Decrypt(password);

                if (model.Email != null && model.Password == decryptedPassword)
                {
                    var getName = db.Users.Where(u => u.Email == model.Email).Select(u => u.Name);
                    var materializeName = getName.ToList();
                    var name = materializeName[0];

                    var getCountry = db.Users.Where(u => u.Email == model.Email).Select(u => u.Country);
                    var materializeCountry = getCountry.ToList();
                    var country = materializeCountry[0];

                    var getEmail = db.Users.Where(u => u.Email == model.Email).Select(u => u.Email);
                    var materializeEmail = getEmail.ToList();
                    var email = materializeEmail[0];

                    // The framework directly takes the full Rol object
                    var userRol = db.Users.Where(x => x.Email == model.Email).Select(x => x.Rol).FirstOrDefault();

                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.Country, country),
                        new Claim(ClaimTypes.Role, userRol.Rol),
                    },
                        "ApplicationCookie");

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);
                    return RedirectToAction("Index", "Product");
                }
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    // Make sure this's a unique identity
                    var checkMail = db.Users.FirstOrDefault(u => u.Email == model.Email);
                    var checkUser = db.Users.FirstOrDefault(u => u.Name == model.Name);
                    var error = false;
                    if(checkMail != null)
                    {
                        error = true;
                        ModelState.AddModelError("", model.Email + " already exists in our database. It must be unique.");
                    }
                    if(checkUser != null)
                    {
                        error = true;
                        ModelState.AddModelError("", model.Name + " has already been registered. Please, look for a new one.");
                    }
                    if(error)
                        return View();
                    
                    var encryptedPassword = CustomEncrypt.Encrypt(model.Password);
                    var user = db.Users.Create();
                    var rol = db.Roles.Where(x => x.Rol == "user").FirstOrDefault();
                    user.Rol = rol; // If we don't add FirstOrDefault() then the type of the Object is IQueryable
                    user.Email = model.Email;
                    user.Password = encryptedPassword;
                    user.Country = model.Country;
                    user.Address = model.Address;
                    user.Name = model.Name;
                    user.FirstName = model.FirstName;
                    user.SecondName = model.SecondName;
                    user.IsActive = true;

                    var defaultShoppingCart = db.Carts.Create();
                    defaultShoppingCart.User = user;
                    defaultShoppingCart.Name = "Shopping cart of " + user.Name + ".";
                    defaultShoppingCart.IsMainCart = true;

                    db.Carts.Add(defaultShoppingCart);
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                    
                        
                    
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }
    }
}