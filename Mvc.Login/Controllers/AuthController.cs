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
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        //POST: Auth - will be called when user clicks submit button on login page
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) //checks if input fields have the correct format and are valid
            {
                //if not, then return the same login view, with the inputs as is, so user doesn't have to retype them
                return View(model);
            }
            //For now, we will authenticate hard-coded credentials
            if (model.Email == "admin@admin.com" && model.Password == "12345")
            {
                //create a list of claims
                List<Claim> claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, "John"),
                    new Claim(ClaimTypes.Email, "john.doe@email.com"),
                    new Claim(ClaimTypes.Country, "Australia")
                };
                //create a claims identity based on above claims, and instruct it to use cookie
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                var owinContext = Request.GetOwinContext();
                var authManager = owinContext.Authentication; //gets the authentication middleware functionality available on the current context
                authManager.SignIn(identity);

                string redirectUrl = GetRedirectUrl(model.ReturnUrl);
                return Redirect(redirectUrl);
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
    }
}