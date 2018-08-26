using Mvc.Login.Models;
using Mvc.Login.Utility;
using Mvc.Login.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Login.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth - will be called to show the login page
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new UserViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        //POST: Auth - will be called when user clicks submit button on login page
        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            if (!ModelState.IsValid) //checks if input fields have the correct format and are valid
            {
                //if not, then return the same login view, with the inputs as is, so user doesn't have to retype them
                return View(model);
            }

            using (var context = new MvcDbContext())
            {
                var user = model.Email!=null? context.Users.FirstOrDefault(u => u.Email == model.Email):null;
                if (user != null)
                {
                    var email = user.Email;
                    var password = user.Password;
                    var decryptedPassword = CustomDecrypt.Decrypt(password);
                    if (model.Email == email && model.Password == decryptedPassword)
                    {
                        //create a list of claims
                        List<Claim> claims = new List<Claim>{
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Country, user.Country)
                        };
                        //create a claims identity based on above claims, and instruct it to use cookie
                        var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                        var owinContext = Request.GetOwinContext();
                        var authManager = owinContext.Authentication; //gets the authentication middleware functionality available on the current context
                        authManager.SignIn(identity);

                        string redirectUrl = GetRedirectUrl(model.ReturnUrl);
                        return Redirect(redirectUrl);
                    }
                }
            }
            return View(model); //if email/psw entered by user is not correct, show the loginpage again
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                //if url is empty or not a local url, return the default home/index route
                return Url.Action("Index", "home");
            }
            //else return the original url
            return returnUrl;
        }

        public ActionResult Logout()
        {
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        /// <summary>
        /// When user hits the registration page
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// When user submits registration
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Registration(UserViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = CustomEncrypt.Encrypt(uservm.Password);
                using (var context = new MvcDbContext())
                {
                    var user = context.Users.Create();
                    user.Email = uservm.Email;
                    user.Password = encryptedPassword;
                    user.Country = uservm.Country;
                    user.Name = uservm.Name;
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields are invalid");
            }
            return View();
        }
    }
}