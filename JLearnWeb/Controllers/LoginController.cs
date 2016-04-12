using BLL;
using JLearnWeb.Extensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(loginFac.login(username, password))
            {
                return RedirectToAction("Index", "Home");
            }

            this.AddNotification("Invalid username or password", NotificationType.ERROR);
            return RedirectToAction("Index", "Login");
        }
    }
}