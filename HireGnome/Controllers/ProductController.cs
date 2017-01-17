using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HireGnome.Models;
using System.Security.Claims;
using System.Data.Entity;

namespace HireGnome.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var db = new MainDbContext();
            return View(db.Products.ToList().Where(u => u.Public == "YES"));
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Products product)
        {
            if (ModelState.IsValid)
            {
                string timeToday = DateTime.Now.ToString("h:mm:ss tt");
                string dateToday = DateTime.Now.ToString("M/dd/yyyy");

                using(var db = new MainDbContext())
                {
                    var dbProduct = db.Products.Create();

                    dbProduct.Name = Request.Form["name"];
                    dbProduct.Details = Request.Form["details"];
                    dbProduct.Price = double.Parse(Request.Form["price"]);
                    dbProduct.Offer = double.Parse(Request.Form["offer"]);
                    dbProduct.AddedDate = dateToday;
                    dbProduct.ModifiedDate = dateToday;

                    string check_public = Request.Form["check_public"];
                    if (check_public != null) // This comes directly from the name of the view Index.csHtml
                    {
                        dbProduct.Public = "YES";
                    }
                    else
                    {
                        dbProduct.Public = "NO";
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
        public ActionResult Edit(int id)
        {
            var db = new MainDbContext();
            var model = new Products();

            model = db.Products.Find(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Products product)
        {
            if (ModelState.IsValid)
            {
                using(var db = new MainDbContext())
                {
                    product.Name = Request.Form["name"];
                    product.Details = Request.Form["details"];
                    product.Price = double.Parse(Request.Form["price"]);
                    product.Offer = double.Parse(Request.Form["offer"]);
                    product.ModifiedDate = DateTime.Now.ToString("M/dd/yyyy");


                    if (Request.Form["check_public"] == null)
                        product.Public = "NO";
                    else
                        product.Public = "YES";



                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
                ModelState.AddModelError("", "Wrong input parameters");
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new MainDbContext();
            var model = db.Products.Find(id);

            if(model == null)
            {
                return HttpNotFound();
            }

            db.Products.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Hide(int id)
        {
            var db = new MainDbContext();
            var model = db.Products.Find(id);

            if (model == null)
                return HttpNotFound();
            model.Public = "NO";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}