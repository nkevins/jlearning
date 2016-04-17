using DAL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL.Facade
{
    public  class UserFacade
    {
        UnitOfWork unitofwork = new UnitOfWork();
        UserRepository usrRepo = new UserRepository();

        public User GetById(int id)
        {
            return unitofwork.UserRepository.GetById(id);
        }

        public bool updateUser(User u)
        {
            try
            {
                unitofwork.UserRepository.Edit(u);
                unitofwork.Save();
                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public List<StudentEnrollment> getStudentEnrollment(int userId)
        {
            try
            {
                return usrRepo.getStudentEnrollment(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentEnrollment> getStudentEnrollmentWithoutLecturerName(int scheduleId)
        {

            try
            {

                return usrRepo.getStudentEnrollmentWithoutLecturerName(scheduleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentEnrollment> getStudentEnrollmentWithLecturerName(int scheduleId)
        {

            try
            {
               
                return usrRepo.getStudentEnrollmentWithLecturerName(scheduleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public List<SelectListItem> getLecturer()
        {
            try
            {
                List<SelectListItem> lstLecturer = usrRepo.getLecturer();
                return lstLecturer;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
