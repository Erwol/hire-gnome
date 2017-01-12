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
    public class HomeController : Controller
    {
        // GET: Home
        // Right click directly in the method's name to create views!
        public ActionResult Index()
        {
            // Returns all the info in DB
            var db = new MainDbContext();

            // SELECT * FROM lists
            return View(db.Lists.ToList());
        }

        // If logged in!
        [HttpPost]
        public ActionResult Index(Lists list)
        {
            if (ModelState.IsValid)
            {
                string timeToday = DateTime.Now.ToString("h:mm:ss tt");
                string dateToday = DateTime.Now.ToString("M/dd/yyyy");
                using (var db = new MainDbContext())
                {
                    // Gets the Email ClaimType and assigned it to the variable sessionEmail
                    Claim sessionEmail = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email);
                    string email = sessionEmail.Value;

                    var userIdQuery = db.Users.Where(u => u.Email == email).Select(u => u.Id);
                    var materializeUserId = userIdQuery.ToList();
                    var userId = materializeUserId[0];

                    var dbList = db.Lists.Create();
                    // dbList.Details = list.Details; Doesn't work after changing from strong type to input text
                    dbList.Details = Request.Form["new_item"];

                    dbList.Date_Posted = list.Date_Posted;
                    dbList.Time_Posted = list.Time_Posted;

                    dbList.User_Id = userId;

                    string check_public = Request.Form["check_public"];
                    if (check_public != null) // This comes directly from the name of the view Index.csHtml
                    {
                        dbList.Public = "YES";
                    }
                    else
                    {
                        dbList.Public = "NO";
                    }

                    db.Lists.Add(dbList);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect form has been placed");
            }
            var listTable = new MainDbContext(); // Prevents from return a null, in addition to delete the required tag
            return View(listTable.Lists.ToList());
        }



        public static string date_posted = "";
        public static string time_posted = "";



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new MainDbContext();
            var model = new Lists();

            model = db.Lists.Find(id);

            date_posted = model.Date_Posted;
            time_posted = model.Time_Posted;


            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Lists list)
        {
            var db = new MainDbContext();
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            string new_item = Request.Form["new_item"];
            string check_public = Request.Form["check_public"];

            if (ModelState.IsValid)
            {
                list.Time_Edited = timeToday;
                list.Date_Edited = dateToday;
                list.Details = new_item;
                list.Time_Posted = time_posted;
                list.Date_Posted = date_posted;
                if (check_public != null) { list.Public = "YES"; }
                else { list.Public = "NO"; }

                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new MainDbContext();
            var model = db.Lists.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            db.Lists.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}