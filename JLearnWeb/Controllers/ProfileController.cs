using BLL.Facade;
using DL;
using JLearnWeb.Constant;
using JLearnWeb.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class ProfileController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProfileController));
        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            DL.User usr = null;

            if (Session[ConstantFields.userSession] != null)
            {
                usr = (DL.User)Session[ConstantFields.userSession];
            }

            return View(usr);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
         [Authorize]
        public ActionResult Edit(int id)
        {

            User usr = null;

            if (Session[ConstantFields.userSession] != null)
            {
                usr = (User)Session[ConstantFields.userSession];
                usr.Password = string.Empty;
            }

            return View(usr);
        }

        // POST: Profile/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(User u)
        {
            try
            {
                u.ObsInd = "N";
                u.Password = PasswordHashUtil.GenerateSaltedHashPwd(u.Password, u.Salt);
                UserFacade usr = new UserFacade();
                usr.updateUser(u);
                Session.Remove(ConstantFields.userSession);
                Session.Add(ConstantFields.userSession, u);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                log.Error("Exceptioin ", ex);
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
