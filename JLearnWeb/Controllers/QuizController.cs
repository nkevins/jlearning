using BLL.Facade;
using DL;
using DL.ModelView;
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
    public class QuizController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(QuizController));
        private QuizFacade _quizFac;

        public QuizController()
        {
            _quizFac = new QuizFacade();
        }

        // GET: Quiz
        public ActionResult Index()
        {
            return View();
        }

        // GET: Quiz/Detail
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult Detail(int id)
        {
            var quiz = _quizFac.GetById(id);
            return View(quiz);
        }

        // GET: Quiz/Live
        public ActionResult Live(int id)
        {
            ViewBag.UserId = User.Identity.GetUserId();
            var quiz = _quizFac.GetById(id);
            return View(quiz);
        }

        // POST: Quiz/Add
        [HttpPost]
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult Add(int scheduleId, string title)
        {
            try
            {
                _quizFac.Add(scheduleId, title);
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
                throw ex;
            }

            return RedirectToAction("Quiz", "Schedule", new { id = scheduleId });
        }

        // GET: Quiz/AddQuestion
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult AddQuestion(int id)
        {
            var quiz = _quizFac.GetById(id);
            return View(quiz);
        }

        [HttpPost]
        [Authorize(Roles = Constant.ConstantFields.Lecturer)]
        public ActionResult AddQuestion(QuizAddQuestionModelView model)
        {
            try
            {
                _quizFac.AddQuestion(model);
            }
            catch(Exception ex)
            {
                log.Error("Exception ", ex);
                throw ex;
            }
            
            return RedirectToAction("Detail", "Quiz", new { id = model.QuizId });
        }
    }
}