using DAL.Repository;
using DL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class CourseController : BaseController<Course, Course>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CourseController));

        public CourseController()
            : base(new Repository<Course>())
        {

        }

        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult DeleteCourse(int id)
        {
            List<Course> lstUsr = new List<Course>();
            Course u = null;
            Course crs = new Course();
          
            try
            {
                if (Session[Constant.ConstantFields.courseList] != null)
                {
                    lstUsr = (List<Course>)Session[Constant.ConstantFields.courseList];
                }

                for (int i = 0; i < lstUsr.Count; i++)
                {
                    u = lstUsr[i];
                    if (u.CourseID == id)
                    {
                        crs.CourseID = u.CourseID;
                        crs.CourseName = u.CourseName;
                        crs.CourseCode = u.CourseCode;
                        crs.Description = u.Description;
                        crs.ObsInd = "Y";
                        break;
                    }
                }

                base.Update(crs);
            }
            catch (Exception ex)
            {
                log.Error("Exception ex ", ex);
            }

            return RedirectToAction("CourseIndex");
        }

        [HttpPost]
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult EditCourse(Course u)
        {
            try
            {
                //Course crs = new Course();
                //crs.CourseID = u.CourseID;
                //crs.CourseCode = u.CourseCode;
                //crs.CourseName = u.CourseName;
                //crs.Description = u.Description;
                //crs.ObsInd = u.ObsInd;
               // u.ObsInd = "N";
                u.CourseCode = u.CourseCode.Trim();
                u.CourseName = u.CourseName.Trim();
                u.Description = u.Description.Trim();
                u.ObsInd = u.ObsInd.Trim();
                base.Update(u);
            }
            catch (Exception ex)
            {
                log.Error("Exceptioin ", ex);

            }

            return RedirectToAction("CourseIndex");
        }

         [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult EditCourse(int id)
        {
            List<Course> lstUsr = new List<Course>();
            Course u = null;
            try
            {
                if (Session[Constant.ConstantFields.courseList] != null)
                {
                    lstUsr = (List<Course>)Session[Constant.ConstantFields.courseList];
                }

                for (int i = 0; i < lstUsr.Count; i++)
                {
                    u = lstUsr[i];
                    if (u.CourseID == id)
                    {
                       
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception ex ", ex);
            }

            return View(u);
        }

        // GET: Course
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult CourseIndex()
        {
            List<Course> lstUsr = base.Index();
            Session.Add(Constant.ConstantFields.courseList, lstUsr);
            return View("Index", lstUsr);
        }

        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult CreateCourse()
        {

            return View();
        }

         [HttpPost]
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult CreateCourse(Course crs)
        {
            try
            {
             
                crs.ObsInd = "N";
                base.Add(crs);
            }
            catch (Exception ex)
            {
                log.Error("Execption ex ", ex);
            }

            return RedirectToAction("CourseIndex");
          
        }
    }
}