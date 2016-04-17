using DAL.Repository;
using DL;
using DL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class QuizFacade
    {
        private IUnitOfWork _uow;

        public QuizFacade()
        {
            _uow = new UnitOfWork();
        }

        public QuizFacade(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Quiz> GetBySchedule(int scheduleId)
        {
            return _uow.QuizRepository.GetAll().Where(x => x.ScheduleID == scheduleId && x.ObsInd == "N").OrderByDescending(x => x.CreatedDate).ToList();
        }

        public Quiz GetById(int quizId)
        {
            return _uow.QuizRepository.GetById(quizId);
        }

        public void AddQuestion(QuizAddQuestionModelView model)
        {
            QuizQuestion question = new QuizQuestion();
            question.Title = model.Question;
            question.QuizID = model.QuizId;
            question.ObsInd = "N";

            QuizChoice choice1 = new QuizChoice();
            choice1.Choice = model.Choice1;
            choice1.ObsInd = "N";
            question.QuizChoices.Add(choice1);            

            QuizChoice choice2 = new QuizChoice();
            choice2.Choice = model.Choice2;
            choice2.ObsInd = "N";
            question.QuizChoices.Add(choice2);            

            QuizChoice choice3 = new QuizChoice();
            choice3.Choice = model.Choice1;
            choice3.ObsInd = "N";
            question.QuizChoices.Add(choice3);          

            QuizChoice choice4 = new QuizChoice();
            choice4.Choice = model.Choice4;
            choice4.ObsInd = "N";
            question.QuizChoices.Add(choice4);

            _uow.QuizQuestionRepository.Insert(question);
            _uow.Save();

            if (model.CorrectAnswer == "choice1")
            {
                question.QuizChoice = choice1;
            }
            else if(model.CorrectAnswer == "choice2")
            {
                question.QuizChoice = choice2;
            }
            else if (model.CorrectAnswer == "choice3")
            {
                question.QuizChoice = choice3;
            }
            else
            {
                question.QuizChoice = choice4;
            }

            _uow.Save();
        }
    }
}
