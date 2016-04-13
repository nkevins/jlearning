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
        private static string SaltString = "SaltString";

        public bool login(string username, string password)
        {
            string saltValue = ConfigurationManager.AppSettings[SaltString].ToString();
            string hashPwd = PasswordHashUtil.GenerateSaltedHashPwd(password, saltValue);

            return true;
        }
    }
}
