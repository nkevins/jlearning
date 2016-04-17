using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL.Facade
{
    public class CourseFacade
    {
        UnitOfWork unitofwork = new UnitOfWork();

        public List<SelectListItem> getCourse()
        {
            try
            {
              
                var query = unitofwork.CourseRepository.GetAll().Select(c => new SelectListItem
                {
                    Value = c.CourseID.ToString(),
                    Text = c.CourseCode + "-" + c.CourseName,
                    //Selected = c.CourseCode.Equals(1)
                });

             
                List<SelectListItem> lstCourse = query.ToList();

              
                return lstCourse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
    }
}
