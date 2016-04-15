
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
        //private static string SaltString = "test";
        UnitOfWork unitofwork = new UnitOfWork();
        User usr = new User();

        public bool login(string username, string password)
        {
            try
            {
                 //string saltValue = ConfigurationManager.AppSettings["SaltString"];
            ///string hashPwd = PasswordHashUtil.GenerateSaltedHashPwd(password, SaltString);
            IQueryable<User> userModel =  unitofwork.UserRepository.GetAll();
            List<User> lstUser = userModel.ToList();

            return compareUserPassword(lstUser, password);

            }catch(Exception ex){
                throw ex;
            }
           
        }

        public bool compareUserPassword(List<User> lstUser, string password)
        {
           
            for (int i = 0; i < lstUser.Count; i++)
            {
                User u = lstUser[i];
                string hashPwd = PasswordHashUtil.GenerateSaltedHashPwd(password, u.Salt);
                if (u.Password.Equals(hashPwd))
                {
                    usr.UserID = u.UserID;
                    return true;
                }
            }

            return false;
        }

        public string getUserRole()
        {
            IQueryable<Role> userRoleModel = unitofwork.UserRoleRepository.GetAll();
            List<Role> lstUser = userRoleModel.ToList();
            string userRole = string.Empty;

            for (int i = 0; i < lstUser.Count; i++)
            {
                Role role = lstUser[i];
                if (role.UserID == usr.UserID)
                {
                    userRole = role.Name;
                }
            }

            return userRole;
        }
    }
}
