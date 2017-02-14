using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HireGnome.Models;
using HireGnome.ViewModels;

namespace HireGnome.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddGnome()
        {
            CreateGnomeViewModel model = new CreateGnomeViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddGnome(CreateGnomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool error = false;
                if(model.Price < 0)
                {
                    error = true;
                    ModelState.AddModelError("", "Price can't be negative.");
                }

                if (model.Offer < 0 || model.Offer > 100)
                {
                    error = true;
                    ModelState.AddModelError("", "The Offer must be between 0% and 100%.");
                }

                if(error)
                    return View();

                using (var db = new MainDbContext())
                {
                    var dbProduct = db.Products.Create();
                    dbProduct.Name = model.Name;
                    dbProduct.Details = model.Details;
                    dbProduct.Price = model.Price;
                    dbProduct.Offer = model.Offer;
                    dbProduct.IsPublic = model.IsPublic;
                    dbProduct.Image = model.Image;
                    dbProduct.CreationDate = DateTime.Now;
                    dbProduct.ModificationDate = DateTime.Now;
                    db.Products.Add(dbProduct);
                    db.SaveChanges();

                    return RedirectToAction("Show", "Product", new { gnome_id = dbProduct.Id });
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Wrong parameter introduced");
                return View();
            }
            
        }


        [HttpGet]
        public ActionResult EditGnome(int id)
        {
            
            using(var db = new MainDbContext())
            {
                var model = db.Products.Find(id);
                if (model == null)
                    return HttpNotFound();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditGnome(Products model)
        {
            if (ModelState.IsValid)
            {
                bool error = false;
                if (model.Price < 0)
                {
                    error = true;
                    ModelState.AddModelError("", "Price can't be negative.");
                }

                if (model.Offer < 0 || model.Offer > 100)
                {
                    error = true;
                    ModelState.AddModelError("", "The Offer must be between 0% and 100%.");
                }

                if (error)
                    return View();

                using (var db = new MainDbContext())
                {
                    var dbProduct = db.Products.Find(model.Id);
                    dbProduct.Name = model.Name;
                    dbProduct.Details = model.Details;
                    dbProduct.Price = model.Price;
                    dbProduct.Offer = model.Offer;
                    dbProduct.IsPublic = model.IsPublic;
                    dbProduct.CreationDate = model.CreationDate;
                    dbProduct.ModificationDate = DateTime.Now;
                    dbProduct.Image = model.Image;
                    dbProduct.Latitude = model.Latitude;
                    dbProduct.Longitude = model.Longitude;
                    db.SaveChanges();

                    return RedirectToAction("Show", "Product", new { gnome_id = dbProduct.Id });
                }

            }
            else
            {
                ModelState.AddModelError("", "Wrong parameter introduced");
                return View();
            }
        }


        [HttpGet]
        public ActionResult DeleteGnome(int id)
        {
            var db = new MainDbContext();
            var model = db.Products.Find(id);

            if (model == null)
                return HttpNotFound();

            // Search all the carts
            for (int i = 0; i < model.Carts.Count; i++)
                model.Carts.ElementAt(i).Products.Remove(model);

            db.Products.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult HideGnome(int id)
        {
            var db = new MainDbContext();
            var model = db.Products.Find(id);

            if (model == null)
                return HttpNotFound();
            if (model.IsPublic)
                model.IsPublic = false;
            else
                model.IsPublic = true;
            db.SaveChanges();
            return RedirectToAction("Show", "Product", new { gnome_id = model.Id });
        }
    }
}