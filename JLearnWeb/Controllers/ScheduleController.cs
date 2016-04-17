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
        private CourseFacade crsFacade;
        private UserFacade usrFacade;
        private ScheduleFacade schFacade;
        private static readonly ILog log = LogManager.GetLogger(typeof(ScheduleController));
        StudentEnrollment en = new StudentEnrollment();

        public ScheduleController()
        {
            _forumThreadFacade = new ForumThreadFacade();
            crsFacade = new CourseFacade();
            usrFacade = new UserFacade();
            schFacade = new ScheduleFacade();
        }

         [Authorize(Roles = "Lecturer")]
        public ActionResult SubmitForm(StudentEnrollment std)
        {
            Schedule sch = new Schedule();
            sch.ObsInd = "N";
            sch.CourseID = std.courseSelected;
            sch.StartDate = std.startDate;
            sch.EndDate = std.endDate;

            schFacade.insertCourseSchedule(sch);
            //UserSchedule usrSch = new UserSchedule();
            //usrSch.UserID = std.lecturerSelected;
            //usrSch.ScheduleID = std.scheduleId;

            CourseSchedule();
            return View("CourseSchedule");
        }

         [Authorize(Roles = "Lecturer")]
        public ActionResult Create()
        {
            List<SelectListItem> items = crsFacade.getCourse();
           // List<SelectListItem> lecturerLst = usrFacade.getLecturer();
            //ViewBag.CourseLst = items;
            
            en.lstCourse = items;
           // en.courseSelected = 1;
            //en.lstLecturer = lecturerLst;
            return View(en);
        }

         [Authorize(Roles = "Lecturer")]
        public ActionResult CourseSchedule()
        {
            List<StudentEnrollment> lst = null;
          
            try
            {
                UserFacade user = new UserFacade();
                lst = user.getCourseSchedule();
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }
            return View(lst);
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
        public ActionResult Forum(int id)
        {
            List<ForumThread> forums = null;
            try
            {
                forums = _forumThreadFacade.GetBySchedule(id);
            }
            catch(Exception ex)
            {
                log.Error("Exception ", ex);
            }
            ViewBag.ScheduleId = id;
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