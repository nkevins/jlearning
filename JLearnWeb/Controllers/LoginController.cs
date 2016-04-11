using BLL;
using JLearnWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class LoginController : Controller
    {

        private LoginFacade loginFac;

        public LoginController()
        {
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
            if(loginFac.login(username, password))
            {
                return RedirectToAction("Index", "Home");
            }

            this.AddNotification("Invalid username or password", NotificationType.ERROR);
            return RedirectToAction("Index", "Login");
        }
    }
}