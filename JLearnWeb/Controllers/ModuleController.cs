using BLL.Facade;
using DL;
using JLearnWeb.Extensions;
using log4net;
using System;
using System.IO;
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
        private DocumentFacade _docFacade;

        public ModuleController()
        {
            _moduleFacade = new ModuleFacade();
            _docFacade = new DocumentFacade();
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

        // POST: AddDOcument
        [HttpPost]
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult AddDocument(int scheduleId, int moduleId, string title)
        {
            string fileName = "";
            string fileExtension = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    fileExtension = Path.GetExtension(file.FileName);

                    if(!_docFacade.isValidExtension(fileExtension))
                    {
                        this.AddNotification("File format is not allowed", NotificationType.ERROR);
                        return RedirectToAction("Module", "Schedule", new { id = scheduleId });
                    }

                    fileName = Guid.NewGuid().ToString() + fileExtension;
                    var path = Path.Combine(Server.MapPath("~/Upload/"), fileName);
                    file.SaveAs(path);
                } else
                {
                    this.AddNotification("Please include a file", NotificationType.ERROR);
                    return RedirectToAction("Module", "Schedule", new { id = scheduleId });
                }
            }

            Document doc = new Document();
            doc.ModuleID = moduleId;
            doc.Title = title;
            doc.FileName = fileName;
            doc.Type = (int) _docFacade.getDocumentType(fileExtension);
            _docFacade.Add(doc);

            this.AddNotification("Document uploaded", NotificationType.SUCCESS);
            return RedirectToAction("Module", "Schedule", new { id = scheduleId });
        }
    }
}