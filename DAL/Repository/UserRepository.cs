using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                         where a.UserID == userId orderby n.StartDate
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

       
    }
}
