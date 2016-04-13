using BLL.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class ScheduleController : Controller
    {
        private ForumThreadFacade _forumThreadFacade;

        public ScheduleController()
        {
            _forumThreadFacade = new ForumThreadFacade();
        }

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
        public ActionResult Forum(int scheduleId)
        {
            var forums = _forumThreadFacade.GetBySchedule(scheduleId);
            return View(forums);
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