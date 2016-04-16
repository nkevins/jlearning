using DAL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public  class UserFacade
    {
        UnitOfWork unitofwork = new UnitOfWork();

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
                UserRepository usrRepo = new UserRepository();
                return usrRepo.getStudentEnrollment(userId);
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
                UserRepository usrRepo = new UserRepository();
                return usrRepo.getStudentEnrollmentWithLecturerName(scheduleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
