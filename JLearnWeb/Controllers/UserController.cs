using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: Student
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult Index()
        {
            return View();
        }
    }
}