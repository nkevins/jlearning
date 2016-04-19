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

        // GET: Quiz/AddQuestion
        public ActionResult AddQuestion(int id)
        {
            var quiz = _quizFac.GetById(id);
            return View(quiz);
        }

        [HttpPost]
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