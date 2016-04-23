using BLL.Facade;
using DL;
using JLearnWeb.Extensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    [Authorize]
    public class ModuleController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ForumController));
        private ModuleFacade _moduleFacade;

        public ModuleController()
        {
            _moduleFacade = new ModuleFacade();
        }

        // POST: Forum
        [HttpPost]
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult Index([Bind(Include = "ModuleName, ScheduleId")] Module module, int scheduleId)
        {
            if (ModelState.IsValid)
            {
                module.ObsInd = "N";
                try
                {
                    _moduleFacade.Add(module);
                }
                catch (Exception ex)
                {
                    log.Error("Exception ", ex);
                    throw ex;
                }

                this.AddNotification("Module added", NotificationType.SUCCESS);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                this.AddNotification(errors[0].First().ErrorMessage, NotificationType.ERROR);
            }

            return RedirectToAction("Module", "Schedule", new { id = scheduleId });
        }
    }
}