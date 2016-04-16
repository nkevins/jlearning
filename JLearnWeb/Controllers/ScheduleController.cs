using BLL.Facade;
using DL;
using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(ScheduleController));

        public ScheduleController()
        {
            _forumThreadFacade = new ForumThreadFacade();
        }

        // GET: Schedule
         [Authorize]
        public ActionResult Index(int id)
        {
            List<StudentEnrollment> lst = null;
            StudentEnrollment std = null;

            try
            {
                UserFacade user = new UserFacade();
                lst = user.getStudentEnrollmentWithLecturerName(id);

                std = lst[0];
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }
            return View(std);
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
            ViewBag.ScheduleId = scheduleId;
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