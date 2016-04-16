using BLL.Facade;
using DL;
using JLearnWeb.Constant;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

         [Authorize]
        public ActionResult Index()
        {
            List<StudentEnrollment> lst = null;

            try
            {
                  if (Session[ConstantFields.userSession] != null)
                  {
                        User usr = (User)Session[ConstantFields.userSession];
                        UserFacade user = new UserFacade();
                        lst = user.getStudentEnrollment(usr.UserID);
                  }
            }
            catch (Exception ex)
            {
                log.Error("Exception ex ",ex);
                
            }

            return View(lst);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}