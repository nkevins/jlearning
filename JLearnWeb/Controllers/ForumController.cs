using BLL.Facade;
using DL;
using JLearnWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLearnWeb.Controllers
{
    public class ForumController : Controller
    {
        private ForumThreadFacade _forumThreadFacade;

        public ForumController()
        {
            _forumThreadFacade = new ForumThreadFacade();
        }

        // GET: Forum
        public ActionResult Index(int id)
        {
            var forum = _forumThreadFacade.GetById(id);
            return View(forum);
        }

        // POST: Forum
        [HttpPost]
        public ActionResult Index([Bind(Include = "Title, ScheduleId")] ForumThread forum, int scheduleId)
        {
            if(ModelState.IsValid)
            {
                forum.ObsInd = "N";
                _forumThreadFacade.Add(forum);
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