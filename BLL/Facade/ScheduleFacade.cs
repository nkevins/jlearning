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
    }
}
