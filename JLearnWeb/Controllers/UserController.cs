using BLL.Facade;
using DAL.Repository;
using DL;
using JLearnWeb.Constant;
using JLearnWeb.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class UserController : BaseController<User,User>
    {
        UserFacade usr = new UserFacade();
        private static readonly ILog log = LogManager.GetLogger(typeof(UserController));

        public UserController()
            : base(new Repository<User>())
        {

        }
        // GET: Student
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult UserIndex()
        {
            List<User> lstUsr = base.Index();
            Session.Add(Constant.ConstantFields.userList, lstUsr);
            return View("Index",lstUsr);
        }

       

        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult EditUser(int id)
        {
            List<User> lstUsr = new List<User>();
            User u = null;
            try
            {
                if (Session[Constant.ConstantFields.userList] != null)
                {
                    lstUsr = (List<User>)Session[Constant.ConstantFields.userList];
                }

                for (int i = 0; i < lstUsr.Count; i++)
                {
                    u = lstUsr[i];
                    if (u.UserID == id)
                    {
                        u.Password = string.Empty;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception ex ", ex);
            }
          
            return View(u);
        }

        [HttpPost]
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult EditUser(User u)
        {
            try
            {
                u.ObsInd = "N";
                u.Password = PasswordHashUtil.GenerateSaltedHashPwd(u.Password, u.Salt);
                base.Update(u);
                //UserFacade usr = new UserFacade();
                //usr.updateUser(u);

                return RedirectToAction("UserIndex");
            }
            catch (Exception ex)
            {
                log.Error("Exceptioin ", ex);
                return View();
            }
        }
    }
}