using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HireGnome.Models;

namespace HireGnome.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddGnome()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGnome(Products gnome)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var dbProduct = db.Products.Create();

                    dbProduct.Name = Request.Form["name"];
                    dbProduct.Details = Request.Form["details"];
                    //dbProduct.Price = double.Parse(Request.Form["price"].Replace(comas con puntos));
                    dbProduct.Price = double.Parse(Request.Form["price"].Replace(',', '.'));
                    dbProduct.Offer = int.Parse(Request.Form["offer"]);

                    string check_public = Request.Form["check_public"];
                    if (check_public != null) // This comes directly from the name of the view Index.csHtml
                    {
                        dbProduct.IsPublic = true;
                    }
                    else
                    {
                        dbProduct.IsPublic = false;
                    }

                    db.Products.Add(dbProduct);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong parameter introduced");
            }
            return View();
        }


        [HttpGet]
        public ActionResult EditGnome(int gnome_id)
        {
            using(var db = new MainDbContext())
            {
                var model = db.Products.Find(gnome_id);
                if (model == null)
                    return HttpNotFound();
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult DeleteGnome(int id)
        {
            var db = new MainDbContext();
            var model = db.Products.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            db.Products.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult HideGnome(int id)
        {
            var db = new MainDbContext();
            var model = db.Products.Find(id);

            if (model == null)
                return HttpNotFound();
            model.IsPublic = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}