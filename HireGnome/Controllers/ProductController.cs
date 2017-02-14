using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HireGnome.Models;
using System.Security.Claims;
using System.Data.Entity;
using HireGnome.ViewModels;
using System.Threading.Tasks;
using PagedList;

namespace HireGnome.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            var db = new MainDbContext();
            PagedList<Products> paged_products;
            if (User.IsInRole("admin"))
                paged_products = new PagedList<Products>(db.Products.ToList(), page, pageSize);
            else
                paged_products = new PagedList<Products>(db.Products.ToList().Where(u => u.IsPublic == true), page, pageSize);

            return View(paged_products);
        }


        public async Task<ActionResult> Search(string searchString)
        {
            using(var db = new MainDbContext())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var gnomes  = db.Products.Where(s => s.Name.Contains(searchString));
                    return View(await gnomes.ToListAsync());
                }
                else
                {
                    return View();
                }

                
            }

            
        }

        public ActionResult GetGnomes()
        {
            using(var db = new MainDbContext())
            {
                List<GnomeMinDataViewModel> gnomes = new List<GnomeMinDataViewModel>();
                List<Products> public_gnomes = db.Products.Where(u => u.IsPublic == true).ToList();
                foreach(Products gnome in public_gnomes)
                {
                    GnomeMinDataViewModel min_gnome = new GnomeMinDataViewModel();
                    min_gnome.Name = gnome.Name;
                    min_gnome.Price = gnome.Price;
                    min_gnome.Offer = gnome.Offer;
                    min_gnome.ReducedPrice = min_gnome.Price * ((double)min_gnome.Offer / 100);
                    min_gnome.Latitude = gnome.Latitude.ToString().Replace(',', '.');
                    min_gnome.Longitude = gnome.Longitude.ToString().Replace(',','.');
                    min_gnome.Image = gnome.Image;
                    min_gnome.Id = gnome.Id;
                    gnomes.Add(min_gnome);
                }
                return Json(gnomes, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpGet]
        public ActionResult Show(int gnome_id)
        {
            using (var db = new MainDbContext())
            {
                var model = db.Products.Find(gnome_id);
                if (model == null || (!model.IsPublic && !User.IsInRole("admin"))) // If the user is an admin, he can see the "disabled" Gnomes
                    return HttpNotFound();
                ShowGnomeViewModel gnome = new ShowGnomeViewModel();
                gnome.Id = model.Id;
                gnome.Image = model.Image;
                gnome.IsPublic = model.IsPublic;
                gnome.Name = model.Name;
                gnome.Offer = model.Offer;
                gnome.Price = model.Price;
                if (gnome.Offer > 0)
                    gnome.ReducedPrice = gnome.Price * ((double)gnome.Offer / 100.0);
                gnome.Details = model.Details;
                gnome.Latitude = model.Latitude.ToString().Replace(',', '.');
                gnome.Longitude = model.Longitude.ToString().Replace(',', '.');
                return View(gnome);
            }
        }
    }
}