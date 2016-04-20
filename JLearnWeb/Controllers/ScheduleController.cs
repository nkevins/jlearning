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
		private QuizFacade _quizFacade;
        private static readonly ILog log = LogManager.GetLogger(typeof(ScheduleController));
        StudentEnrollment en = new StudentEnrollment();

        public ScheduleController()
        {
            _forumThreadFacade = new ForumThreadFacade();
			_quizFacade = new QuizFacade();
            crsFacade = new CourseFacade();
            usrFacade = new UserFacade();
            schFacade = new ScheduleFacade();
        }

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]

        public ActionResult deleteCourseSchedule(int id)
        {
            try
            {
                StudentEnrollment stdmModel = new StudentEnrollment();

                if (Session[Constant.ConstantFields.courseSchedule] != null)
                {
                    List<StudentEnrollment> lst = (List<StudentEnrollment>)Session[Constant.ConstantFields.courseSchedule];

                    for (int i = 0; i < lst.Count; i++)
                    {
                        StudentEnrollment std = lst[i];
                        if (std.scheduleId == id)
                        {
                            stdmModel = std;
                           
                            break;
                        }
                    }
                }

                Schedule sch = new Schedule();
                sch.ObsInd = "Y";
                sch.CourseID = stdmModel.courseId;
                sch.StartDate = stdmModel.startDate;
                sch.EndDate = stdmModel.endDate;
                sch.ScheduleID = stdmModel.scheduleId;

                schFacade.updateCourseSchedule(sch);

                CourseSchedule();
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }

            return View("CourseSchedule");
        }

        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult EditForm(StudentEnrollment std)
        {
            try
            {
                Schedule sch = new Schedule();

                if (TempData[Constant.ConstantFields.scheduleID] != null)
                {
                    sch.ScheduleID = (int) TempData[Constant.ConstantFields.scheduleID];
                }
               
                 if (TempData[Constant.ConstantFields.courseID] != null)
                {
                    sch.CourseID = (int)TempData[Constant.ConstantFields.courseID];
                }

                sch.StartDate = std.startDate;
                sch.EndDate = std.endDate;
                sch.ObsInd = "N";

                schFacade.updateCourseSchedule(sch);
                CourseSchedule();

            }
            catch (Exception ex)
            {
                log.Error("Exception ex", ex);
            }
            
            return View("CourseSchedule");
        }

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult EditLecturerForm(StudentEnrollment std)
        {
            try
            {
                UserSchedule usrSch = new UserSchedule();

                if (TempData[Constant.ConstantFields.usrScheduleID] != null)
                {
                    usrSch.UserScheduleID = (int) TempData[Constant.ConstantFields.usrScheduleID];
                }

                if (TempData[Constant.ConstantFields.scheduleID] != null)
                {
                    usrSch.ScheduleID = (int) TempData[Constant.ConstantFields.scheduleID];
                }

                usrSch.UserID = std.lecturerSelected;
                usrSch.ObsInd = "N";

                if (TempData[Constant.ConstantFields.lecturerName] == null)
                {
                    schFacade.insertLectureSchedule(usrSch);
                }
                else
                {
                    schFacade.updateLectureSchedule(usrSch);
                }
               
                LecturerSchedule();
            }
            catch (Exception ex)
            {
                log.Error("Exception ex ", ex);
            }
           
            return View("LecturerSchedule");
        }

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult SubmitForm(StudentEnrollment std)
        {
            try
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
              
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }

            return View("CourseSchedule");
        }

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]
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

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]
         public ActionResult EditLecturerSchedule(int id)
         {
             StudentEnrollment stdmModel = new StudentEnrollment();
             List<SelectListItem> lecturerLst = usrFacade.getLecturer();

             try
             {
                 if (Session[Constant.ConstantFields.lecturerSchedule] != null)
                 {
                     List<StudentEnrollment> lst = (List<StudentEnrollment>) Session[Constant.ConstantFields.lecturerSchedule];

                     for (int i = 0; i < lst.Count; i++)
                     {
                         StudentEnrollment std = lst[i];
                         if (std.scheduleId == id)
                         {
                             stdmModel = std;
                             stdmModel.lecturerSelected = (Int16)std.userId;
                             TempData[Constant.ConstantFields.usrScheduleID] = id;
                             TempData[Constant.ConstantFields.scheduleID] = std.scheduleId;
                             TempData[Constant.ConstantFields.lecturerName] = std.lecturerName;
                             stdmModel.lstLecturer = lecturerLst;
                             //TempData[Constant.ConstantFields.courseID] = std.courseId;
                         }
                     }

                 }
             }
             catch (Exception ex)
             {
                 log.Error("Exception ex ", ex);
             }

             return View(stdmModel);
         }

        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
         public ActionResult EditCourseSchedule(int id)
         {
             StudentEnrollment stdmModel = new StudentEnrollment();

             try
             {
                 if (Session[Constant.ConstantFields.courseSchedule] != null)
                 {
                     List<StudentEnrollment> lst = (List<StudentEnrollment>)Session[Constant.ConstantFields.courseSchedule];

                     for (int i = 0; i < lst.Count; i++)
                     {
                         StudentEnrollment std = lst[i];
                         if (std.scheduleId == id)
                         {
                             stdmModel = std;
                             TempData[Constant.ConstantFields.scheduleID] = id;
                             TempData[Constant.ConstantFields.courseID] = std.courseId;
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 log.Error("Exception ex ", ex);
             }
        
             return View(stdmModel);
         }

        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult StudentSchedule()
        {
            List<StudentEnrollment> lst = null;
            List<StudentEnrollment> lst1 = null;
            List<StudentEnrollment> finalLst = new List<StudentEnrollment>();
            try
            {
                lst = schFacade.getStudentSchedule();
                lst1 = schFacade.getStudentCfmSchedule();

                for (int i = 0; i < lst.Count; i++)
                {
                    StudentEnrollment s1 = new StudentEnrollment();
                    s1 = lst[i];

                    for (int j = 0; j < lst1.Count; j++)
                    {
                        StudentEnrollment s2 = new StudentEnrollment();
                        s2 = lst1[j];
                        if (s1.usrScheduleId == s2.usrScheduleId)
                        {
                            s1.studentName = s2.studentName;
                            s1.userId = s2.userId;

                            s1.usrScheduleId = s2.usrScheduleId;
                            break;
                        }
                    }

                    if (s1.studentName != null)
                    {
                        finalLst.Add(s1);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }

            Session.Add(Constant.ConstantFields.lecturerSchedule, finalLst);
            return View(finalLst);
        }

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult LecturerSchedule()
        {
            List<StudentEnrollment> lst = null;
            List<StudentEnrollment> lst1 = null;
            List<StudentEnrollment> finalLst = new List<StudentEnrollment>();
            try
            {
                lst = schFacade.getLecturerSchedule();
                lst1 = schFacade.getLecturerCfmSchedule();

                for (int i = 0; i < lst.Count; i++)
                {
                    StudentEnrollment s1 = new StudentEnrollment();
                       s1 =  lst[i];

                    for (int j = 0; j < lst1.Count; j++)
                    {
                        StudentEnrollment s2 = new StudentEnrollment();
                        s2 = lst1[j];
                        if (s1.scheduleId == s2.scheduleId)
                        {
                            s1.lecturerName = s2.lecturerName;
                            s1.userId = s2.userId;
                            
                            s1.usrScheduleId = s2.usrScheduleId;
                            break;
                        }
                    }
                    finalLst.Add(s1);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception ",ex);
            }

            Session.Add(Constant.ConstantFields.lecturerSchedule, finalLst);
            return View(lst);
        }

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult CourseSchedule()
        {
            List<StudentEnrollment> lst = null;
          
            try
            {
           
                lst = schFacade.getCourseSchedule();
                Session.Add(Constant.ConstantFields.courseSchedule, lst);
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

                if (lst == null || lst.Count == 0)
                {
                    lst = user.getStudentEnrollmentWithoutLecturerName(id);
                }

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

        // GET: Schedule/Quiz
        public ActionResult Quiz(int id)
        {
            List<Quiz> quizes = null;
            try
            {
                quizes = _quizFacade.GetBySchedule(id);
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }
            ViewBag.ScheduleId = id;
            return View(quizes);
        }
    }
}