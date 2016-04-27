
using DAL.Repository;
using DL;
using JLearnWeb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Facade
{
    public class LoginFacade
    {
        IUnitOfWork unitofwork = new UnitOfWork();
        private Dictionary<int, User> usr;

        public LoginFacade()
        {

        }

        public LoginFacade(IUnitOfWork uow)
        {
            usr = new Dictionary<int, User>();
            unitofwork = uow;
        }

        public string computePassword(string password, string salt)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            return crypto.Compute(password, salt);
        }

        public bool compareUsrPassword(string userpwd, string inputpassword)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            return crypto.Compare(userpwd, inputpassword);
        }

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

                string inputPassword = computePassword(password, usr.Salt);
                if (!compareUsrPassword(usr.Password, inputPassword))
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
            var role = unitofwork.UserRepository.GetById(userId).Roles.FirstOrDefault();

            if (role == null)
            {
                return "";
            }

            return role.Name;
        }
    }
}
