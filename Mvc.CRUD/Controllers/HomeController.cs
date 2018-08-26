using Mvc.CRUD.Models;
using Mvc.CRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Mvc.CRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<ListViewModel> list = GetListItems();
            return View(list);
        }

        private static List<ListViewModel> GetListItems()
        {
            List<ListViewModel> list = new List<ListViewModel>();
            using (var dbContext = new MvcDbContext())
            {
                list = dbContext.Lists.Select(item => new ListViewModel
                {
                    Id = item.Id,
                    Details = item.Details,
                    Date_Edited = item.Date_Edited,
                    Time_Edited = item.Time_Edited,
                    Date_Posted = item.Date_Posted,
                    Time_Posted = item.Time_Posted,
                    Public = item.Public == "YES"
                }).ToList();
            }
            return list;
        }

        [HttpPost]
        public ActionResult Index(ListViewModel model)
        {
            if (ModelState.IsValid)
            {
                string timeToday = DateTime.Now.ToString("h:mm:ss tt");
                string dateToday = DateTime.Now.ToString("M/dd/yyyy");

                Claim sessionEmail = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email);
                string userEmail = sessionEmail.Value;
                string text_details = Request.Form["text_details"];
                string check_public = Request.Form["check_public"];
                using (var dbContext = new MvcDbContext())
                {
                    Users user = dbContext.Users.FirstOrDefault(usr => usr.Email == userEmail);
                    Lists list = dbContext.Lists.Create();
                    list.Details = text_details;
                    list.Date_Posted = dateToday;
                    list.Time_Posted = timeToday;
                    if (user != null)
                    {
                        list.User_Id = user.Id;
                        if (check_public != null)
                        {
                            list.Public = "YES";
                        }
                        else
                        {
                            list.Public = "NO";
                        }
                        dbContext.Lists.Add(list);
                        dbContext.SaveChanges();
                        ModelState.Clear();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields are having an incorrect format");
            }

            List<ListViewModel> listItems = GetListItems();
            return View(listItems);
        }

        public ActionResult Edit(int id)
        {
            ListViewModel model = new ListViewModel();
            using (var dbContext = new MvcDbContext())
            {
                var listItem = dbContext.Lists.Find(id);
                model.Details = listItem.Details;
                model.Id = listItem.Id;
                model.Public = listItem.Public == "YES";
                model.Date_Posted = listItem.Date_Posted;
                model.Time_Posted = listItem.Time_Posted;
                model.Date_Edited = listItem.Date_Edited;
                model.Time_Edited = listItem.Time_Edited;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ListViewModel model)
        {
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            string text_details = Request.Form["text_details"];
            string check_public = Request.Form["check_public"];
            if (ModelState.IsValid)
            {
                using (var dbContext = new MvcDbContext())
                {
                    Lists list = dbContext.Lists.Find(model.Id);
                    list.Time_Edited = timeToday;
                    list.Date_Edited = dateToday;
                    list.Details = text_details;
                    list.Public = check_public != null ? "YES" : "NO";
                    dbContext.Entry(list).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            using (var dbContext = new MvcDbContext())
            {
                var model = dbContext.Lists.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                dbContext.Lists.Remove(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}