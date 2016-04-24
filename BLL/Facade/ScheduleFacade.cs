using DAL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class ScheduleFacade
    {
        UnitOfWork unitofwork = new UnitOfWork();
        UserRepository usrRepo = new UserRepository();

        public bool insertCourseSchedule(Schedule u)
        {
            try
            {
                unitofwork.SchRepo.Insert(u);
                unitofwork.Save();
                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<StudentEnrollment> getStudentCfmScheduleByID(int ID)
        {
            try
            {
                return usrRepo.getStudentSchedule(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StudentEnrollment setModel(StudentEnrollment a)
        {
            StudentEnrollment obj = new StudentEnrollment();
            obj.userId = (int)a.userId;
            obj.lecturerName = a.lecturerName;
            obj.usrScheduleId = a.usrScheduleId;
            obj.scheduleId = (int)a.scheduleId;
            obj.courseCode = a.courseCode;
            obj.courseName = a.courseName;
            obj.endDate = a.endDate;
            obj.startDate = a.startDate;
            return obj;
        }

        public List<StudentEnrollment> getLecturerCfmSchedule()
        {
            try
            {
                return usrRepo.getLecturerSchedule();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<StudentEnrollment> getStudentScheduleByID(int ID)
        {
            return usrRepo.getStudentScheduleEnlById(ID);
        }

      

        public List<StudentEnrollment> getLecturerSchedule()
        {

            try
            {
                return usrRepo.getCourseSchedule();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentEnrollment> getCourseSchedule()
        {

            try
            {
                return usrRepo.getCourseSchedule();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool updateCourseSchedule(Schedule u)
        {
            try
            {
                unitofwork.SchRepo.Edit(u);
                unitofwork.Save();
                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool isUserEnrolled(int userid, int scheduleId)
        {
            try
            {
                List<UserSchedule> entities = unitofwork.UsrSchRepo.GetAll().Where((x => x.ScheduleID == scheduleId && x.UserID == userid)).ToList();

               if (entities == null || entities.Count == 0)
               {
                   return false;
               }

                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool updateLectureSchedule(UserSchedule u)
        {
            try
            {
                unitofwork.UsrSchRepo.Edit(u);
                unitofwork.Save();
                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool insertLectureSchedule(UserSchedule u)
        {
            try
            {
                unitofwork.UsrSchRepo.Insert(u);
                unitofwork.Save();
                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
