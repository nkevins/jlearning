
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
       

        public User login(string username, string password)
        {
            User usr = null;
            try
            {
                 //string saltValue = ConfigurationManager.AppSettings["SaltString"];
            ///string hashPwd = PasswordHashUtil.GenerateSaltedHashPwd(password, SaltString);
            IQueryable<User> userModel =  unitofwork.UserRepository.GetAll();
            List<User> lstUser = userModel.ToList();

            usr = getUserAccount(lstUser, username,password);

            }catch(Exception ex){
                throw ex;
            }

            return usr;
        }

        public User getUserAccount(List<User> lstUser, string username, string password)
        {
            User user = null;
            for (int i = 0; i < lstUser.Count; i++)
            {
                User u = lstUser[i];
                string hashPwd = PasswordHashUtil.GenerateSaltedHashPwd(password, u.Salt);
                if (u.Email.Equals(username) && u.Password.Equals(hashPwd))
                {
                    user = u;
                    break;
                }
            }

            return user;
        }

        public string getUserRole(int userId)
        {
            IQueryable<Role> userRoleModel = unitofwork.UserRoleRepository.GetAll();
            List<Role> lstUser = userRoleModel.ToList();
            string userRole = string.Empty;

            for (int i = 0; i < lstUser.Count; i++)
            {
                Role role = lstUser[i];
                if (role.UserID == userId)
                {
                    userRole = role.Name;
                }
            }

            return userRole;
        }
    }
}
