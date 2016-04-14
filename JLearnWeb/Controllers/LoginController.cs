using BLL;
using JLearnWeb.Extensions;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class LoginController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));
        private LoginFacade loginFac;

        public LoginController()
        {
            log.Info("Test");
            loginFac = new LoginFacade();
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            try
            {
                if (loginFac.login(username, password))
                {
                    string userRole = loginFac.getUserRole();
                    setIdentityRole(username, userRole);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception Login ", ex);
            }

            log.Error("Login failed for this user id " + username);
            this.AddNotification("Invalid username or password", NotificationType.ERROR);
            return RedirectToAction("Index", "Login");
        }

        public void setIdentityRole(string userName, string userRole)
        {
            var ident = new ClaimsIdentity(
          new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, userName),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

              new Claim(ClaimTypes.Name,userName),

              // optionally you could add roles if any
              new Claim(ClaimTypes.Role, userRole),
             
          },
          DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.GetOwinContext().Authentication.SignIn(
               new AuthenticationProperties { IsPersistent = true }, ident);
        }
    }
}