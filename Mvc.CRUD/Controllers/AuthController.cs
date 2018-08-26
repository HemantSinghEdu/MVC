using Microsoft.Owin;
using Microsoft.Owin.Security;
using Mvc.CRUD.Models;
using Mvc.CRUD.Utility;
using Mvc.CRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Mvc.CRUD.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public ActionResult Index()
        {
            List<ListViewModel> list = GetPublicListItems();
            return View(list);
        }

        private static List<ListViewModel> GetPublicListItems()
        {
            List<ListViewModel> list = new List<ListViewModel>();
            using (var dbContext = new MvcDbContext())
            {
                list = 
                    dbContext.Lists
                    .Where(item => item.Public=="YES")
                    .Select(item => new ListViewModel
                    {
                        Id = item.Id,
                        Details = item.Details,
                        Date_Edited = item.Date_Edited,
                        Time_Edited = item.Time_Edited,
                        Date_Posted = item.Date_Posted,
                        Time_Posted = item.Time_Posted,
                        Public = item.Public == "YES"
                    })
                    .ToList();
            }
            return list;
        }

        /// <summary>
        /// GET action that shows the login page
        /// redirectUrl stores the original requested url when 
        /// the user was redirected to this login page
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public ActionResult Login(string redirectUrl)
        {
            UserViewModel model = new UserViewModel
            {
                ReturnUrl = redirectUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //handle the error message: return to the login page
                return View(model);
            }

            using (var context = new MvcDbContext())
            {
                //fetch the user details from db and validate user
                string email = model.Email;
                string password = model.Password;

                Users user = context.Users.FirstOrDefault(usr => usr.Email == email);
                if (user != null)
                {
                    var encryptedPassword = user.Password;
                    var decryptedPassword = CustomDecrypt.Decrypt(encryptedPassword);
                    if (password == decryptedPassword)
                    {
                        List<Claim> claims = new List<Claim>{
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Country, user.Country)
                    };

                        ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie");

                        IOwinContext owinContext = Request.GetOwinContext();
                        IAuthenticationManager authManager = owinContext.Authentication;
                        authManager.SignIn(identity);

                        string redirectUrl = GetRedirectUrl(model.ReturnUrl);
                        return Redirect(redirectUrl);
                    }
                }
            }
            return View(model);
        }

        private string GetRedirectUrl(string redirectUrl)
        {
            if (string.IsNullOrEmpty(redirectUrl))
            {
                return Url.Action("Index", "Home");
            }
            return redirectUrl;
        }

        public ActionResult Logout()
        {
            IOwinContext owinContext = Request.GetOwinContext();
            IAuthenticationManager authManager = owinContext.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var password = model.Password;
                var encryptedPassword = CustomEncrypt.Encrypt(password);

                using (var context = new MvcDbContext())
                {
                    var userAlreadyExists = context.Users.Any(usr => usr.Email == model.Email);
                    if (userAlreadyExists)
                    {
                        return RedirectToAction("Registration");
                    }
                    Users user = context.Users.Create();
                    user.Email = model.Email;
                    user.Password = encryptedPassword;
                    user.Name = model.Name;
                    user.Country = model.Country;

                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return RedirectToAction("Login","Auth");
            }

            ModelState.AddModelError("", "One or more fields are invalid");
            return View();
        }
    }
}