using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Repository
{
    public class UserRepository
    {

        public List<StudentEnrollment> getStudentEnrollment(int userId)
        {
           
            List<StudentEnrollment> c = new List<StudentEnrollment>();
            JLearnDBEntities db = new JLearnDBEntities();
            var query = (from m in db.UserSchedules
                         join n in db.Schedules on m.ScheduleID equals n.ScheduleID
                         join x in db.Courses on n.CourseID equals x.CourseID
                         join a in db.Users on m.UserID equals a.UserID
                         where a.UserID == userId && m.ObsInd == "N" orderby n.StartDate
                         select new { courseCode = x.CourseCode, courseName = x.CourseName, startDate  = n.StartDate, endDate =n.EndDate, scheduleId = n.ScheduleID});

            try
            {
                foreach (var a in query)
                {
                    StudentEnrollment obj = new StudentEnrollment();
                    obj.courseCode = a.courseCode;
                    obj.courseName = a.courseName;
                    obj.scheduleId = a.scheduleId;
                    obj.startDate = a.startDate;
                    obj.endDate = a.endDate;
                    c.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

        public List<StudentEnrollment> getStudentEnrollmentWithoutLecturerName(int scheduleId)
        {

            List<StudentEnrollment> c = new List<StudentEnrollment>();
            JLearnDBEntities db = new JLearnDBEntities();
            var query = (from 
                          n in db.Schedules 
                         join x in db.Courses on n.CourseID equals x.CourseID
               
                         where n.ScheduleID == scheduleId
                         && n.ObsInd == "N"
                         orderby n.StartDate
                         select new
                         {
                             courseCode = x.CourseCode,
                             courseName = x.CourseName,
                             startDate = n.StartDate,
                             endDate = n.EndDate,
                             scheduleId = n.ScheduleID,
                             lecturerName = string.Empty,
                             description = x.Description
                         });

            try
            {
                foreach (var a in query)
                {
                    StudentEnrollment obj = new StudentEnrollment();
                    obj.courseCode = a.courseCode;
                    obj.courseName = a.courseName;
                    obj.scheduleId = a.scheduleId;
                    obj.startDate = a.startDate;
                    obj.endDate = a.endDate;
                    obj.lecturerName = a.lecturerName;
                    obj.description = a.description;
                    c.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

        public List<StudentEnrollment> getStudentEnrollmentWithLecturerName(int scheduleId)
        {

            List<StudentEnrollment> c = new List<StudentEnrollment>();
            JLearnDBEntities db = new JLearnDBEntities();
            var query = (from m in db.UserSchedules
                         join n in db.Schedules on m.ScheduleID equals n.ScheduleID
                         join x in db.Courses on n.CourseID equals x.CourseID
                         join a in db.Users on m.UserID equals a.UserID
                         join role in db.Roles on a.UserID equals role.UserID
                         where n.ScheduleID == scheduleId
                         && role.Name == "Lecturer" && n.ObsInd == "N"
                         orderby n.StartDate
                         select new { courseCode = x.CourseCode, courseName = x.CourseName, startDate = n.StartDate, 
                             endDate = n.EndDate, scheduleId = n.ScheduleID,lecturerName = a.Name,description = x.Description });

            try
            {
                foreach (var a in query)
                {
                    StudentEnrollment obj = new StudentEnrollment();
                    obj.courseCode = a.courseCode;
                    obj.courseName = a.courseName;
                    obj.scheduleId = a.scheduleId;
                    obj.startDate = a.startDate;
                    obj.endDate = a.endDate;
                    obj.lecturerName = a.lecturerName;
                    obj.description = a.description;
                    c.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

        public List<StudentEnrollment> getLecturerSchedule()
        {

            List<StudentEnrollment> c = new List<StudentEnrollment>();
            JLearnDBEntities db = new JLearnDBEntities();
            var query = (from m in db.UserSchedules
                         join n in db.Schedules on m.ScheduleID equals n.ScheduleID
                         join a in db.Users on m.UserID equals a.UserID
                         join role in db.Roles on a.UserID equals role.UserID
                         where role.Name == "Lecturer" && n.ObsInd == "N" && m.ObsInd == "N"
                         orderby n.StartDate
                         select new
                         {
                             scheduleId = m.ScheduleID,
                             usrscheduleId = m.UserScheduleID,
                             lecturerName = a.Name,
                             userId = m.UserID
                            
                         });


            try
            {
                foreach (var a in query)
                {
                    StudentEnrollment obj = new StudentEnrollment();
                    obj.userId = (int) a.userId;
                    obj.lecturerName = a.lecturerName;
                    obj.usrScheduleId = a.usrscheduleId;
                    obj.scheduleId = (int)a.scheduleId;
                    c.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

        public List<StudentEnrollment> getStudentSchedule(int ID)
        {

            List<StudentEnrollment> c = new List<StudentEnrollment>();
            JLearnDBEntities db = new JLearnDBEntities();
            var query = (from m in db.UserSchedules
                         join n in db.Schedules on m.ScheduleID equals n.ScheduleID
                         join a in db.Users on m.UserID equals a.UserID
                         join role in db.Roles on a.UserID equals role.UserID
                         where role.Name == "Student" && n.ObsInd == "N" && m.ObsInd == "N"
                         orderby n.StartDate
                         select new
                         {
                             scheduleId = m.ScheduleID,
                             usrscheduleId = m.UserScheduleID,
                             stdName = a.Name,
                             userId = m.UserID

                         });

            if (ID > 0)
            {
                query = query.Where(n => n.scheduleId == ID);
            }
           
            try
            {
                foreach (var a in query)
                {
                    StudentEnrollment obj = new StudentEnrollment();
                    obj.userId = (int)a.userId;
                    obj.studentName = a.stdName;
                    obj.usrScheduleId = a.usrscheduleId;
                    obj.scheduleId = (int)a.scheduleId;
                    c.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

     
        public List<StudentEnrollment> getCourseSchedule()
        {

            List<StudentEnrollment> c = new List<StudentEnrollment>();
            JLearnDBEntities db = new JLearnDBEntities();
            //var query = (from m in db.UserSchedules
            //             join n in db.Schedules on m.ScheduleID equals n.ScheduleID
            //             join x in db.Courses on n.CourseID equals x.CourseID
            //             join a in db.Users on m.UserID equals a.UserID
            //             join role in db.Roles on a.UserID equals role.UserID
            //             where role.Name == "Lecturer" && n.ObsInd == "N"
            //             orderby n.StartDate
            //             select new
            //             {
            //                 courseCode = x.CourseCode,
            //                 courseName = x.CourseName,
            //                 startDate = n.StartDate,
            //                 endDate = n.EndDate,
            //                 scheduleId = n.ScheduleID,
            //                 lecturerName = a.Name,
            //                 description = x.Description
            //             });

            var query = (from 
                          n in db.Schedules 
                         join x in db.Courses on n.CourseID equals x.CourseID
                         where  n.ObsInd == "N"
                         orderby n.StartDate
                         select new
                         {
                             courseCode = x.CourseCode,
                             courseName = x.CourseName,
                             startDate = n.StartDate,
                             endDate = n.EndDate,
                             scheduleId = n.ScheduleID,
                             courseId = n.CourseID,
                             description = x.Description
                         });

            try
            {
                foreach (var a in query)
                {
                    StudentEnrollment obj = new StudentEnrollment();
                    obj.courseCode = a.courseCode;
                    obj.courseName = a.courseName;
                    obj.scheduleId = a.scheduleId;
                    obj.startDate = a.startDate;
                    obj.endDate = a.endDate;
                    obj.courseId = (int) a.courseId;
                    obj.description = a.description;
                    c.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

        public List<StudentEnrollment> getStudentScheduleEnlById(int ID)
        {

            List<StudentEnrollment> c = new List<StudentEnrollment>();
            JLearnDBEntities db = new JLearnDBEntities();

            var query = (from
                          n in db.Schedules
                         join y in db.UserSchedules on n.ScheduleID equals y.ScheduleID
                         join x in db.Courses on n.CourseID equals x.CourseID
                         where n.ObsInd == "N" && y.ObsInd == "N"
                         orderby n.StartDate
                         select new
                         {
                             courseCode = x.CourseCode,
                             courseName = x.CourseName,
                             startDate = n.StartDate,
                             endDate = n.EndDate,
                             scheduleId = n.ScheduleID,
                             courseId = n.CourseID,
                             description = x.Description,
                             userSchID = y.UserScheduleID,
                             userId = y.UserID
                         });
            if (ID > 0)
            {
                query = query.Where(n => n.scheduleId == ID);
            }
           

            try
            {
                foreach (var a in query)
                {
                    StudentEnrollment obj = new StudentEnrollment();
                    obj.courseCode = a.courseCode;
                    obj.courseName = a.courseName;
                    obj.scheduleId = a.scheduleId;
                    obj.startDate = a.startDate;
                    obj.endDate = a.endDate;
                    obj.courseId = (int)a.courseId;
                    obj.description = a.description;
                    obj.usrScheduleId = a.userSchID;
                    obj.userId = (int) a.userId;
                    c.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

        

        public List<SelectListItem> getLecturer()
        {
            try
            {
                 JLearnDBEntities db = new JLearnDBEntities();
                 List<SelectListItem> lstLecturer = new List<SelectListItem>();
                 var query = (from
                              a in db.Users
                              join role in db.Roles on a.UserID equals role.UserID
                              where role.Name == "Lecturer" && a.ObsInd == "N"
                            
                              select new
                              {
                                 userId = a.UserID,
                                 name = a.Name,
                              });

                 foreach (var a in query)
                 {
                     lstLecturer.Add(new SelectListItem { Text = a.userId + "-" + a.name, Value = a.userId.ToString() });
                 }

                 return lstLecturer;
            }catch(Exception ex){
                throw ex;
            }
           
        }

        public List<SelectListItem> getStudent()
        {
            try
            {
                JLearnDBEntities db = new JLearnDBEntities();
                List<SelectListItem> lstStudent = new List<SelectListItem>();
                var query = (from
                             a in db.Users
                             join role in db.Roles on a.UserID equals role.UserID
                             where role.Name == "Student" && a.ObsInd == "N"

                             select new
                             {
                                 userId = a.UserID,
                                 name = a.Name,
                             });

                foreach (var a in query)
                {
                    lstStudent.Add(new SelectListItem { Text = a.userId + "-" + a.name, Value = a.userId.ToString() });
                }

                return lstStudent;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
