using BLL.Facade;
using DL;
using JLearnWeb.Extensions;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));
        private ForumThreadFacade _forumThreadFacade;

        public ForumController()
        {
            _forumThreadFacade = new ForumThreadFacade();
        }

        // GET: Forum
        public ActionResult Index(int id)
        {
            ForumThread forum = null;
            try
            {
                forum = _forumThreadFacade.GetById(id);
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }
            ViewBag.UserId = User.Identity.GetUserId();
            return View(forum);
        }

        // POST: Forum
        [HttpPost]
        public ActionResult Index([Bind(Include = "Title, ScheduleId")] ForumThread forum, int scheduleId)
        {
            if(ModelState.IsValid)
            {
                forum.ObsInd = "N";
                try
                {
                    _forumThreadFacade.Add(forum);
                }
                catch(Exception ex)
                {
                    log.Error("Exception ", ex);
                    throw ex;
                }
                
                this.AddNotification("Forum added", NotificationType.SUCCESS);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                this.AddNotification(errors[0].First().ErrorMessage, NotificationType.ERROR);
            }
            
            return RedirectToAction("Forum", "Schedule", new { scheduleId = scheduleId });
        }
    }
}