using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        // GET: Schedule/Module
        public ActionResult Module()
        {
            return View();
        }

        // GET: Schedule/Forum
        public ActionResult Forum()
        {
            return View();
        }

        // GET: Schedule/Thread
        public ActionResult Thread()
        {
            return View();
        }

        // GET: Schedule/Quiz
        public ActionResult Quiz()
        {
            return View();
        }
    }
}