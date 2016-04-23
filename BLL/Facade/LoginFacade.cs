
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
                usr = unitofwork.UserRepository.GetAll().Where(x => x.Email == username).FirstOrDefault();
                if (usr == null)
                {
                    return null;
                }

                var crypto = new SimpleCrypto.PBKDF2();
                string inputPassword = crypto.Compute(password, usr.Salt);
                if(!crypto.Compare(usr.Password, inputPassword))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usr;
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
