
using DAL.Repository;
using DL;
using JLearnWeb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class LoginFacade
    {
        private static string SaltString = "test";
        UnitOfWork unitofwork = new UnitOfWork();
       
        public bool login(string username, string password)
        {
            try
            {
                 //string saltValue = ConfigurationManager.AppSettings["SaltString"];
            string hashPwd = PasswordHashUtil.GenerateSaltedHashPwd(password, SaltString);
            IQueryable<User> userModel =  unitofwork.UserRepository.GetAll();
            List<User> lstUser = userModel.ToList();

            return compareUserPassword(lstUser, hashPwd);

            }catch(Exception ex){
                throw ex;
            }
           
        }

        public bool compareUserPassword(List<User> lstUser, string hashPwd)
        {
            for (int i = 0; i < lstUser.Count; i++)
            {
                User u = lstUser[i];

                if (u.Password.Equals(hashPwd))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
