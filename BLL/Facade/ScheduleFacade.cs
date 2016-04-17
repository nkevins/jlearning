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
    }
}
