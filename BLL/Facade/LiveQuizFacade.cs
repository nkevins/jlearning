using DAL.Repository;
using DL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class LiveQuizFacade
    {
        private Dictionary<int, QuizQuestion> _quiz;
        private IUnitOfWork _uow;

        public LiveQuizFacade()
        {
            _quiz = new Dictionary<int, QuizQuestion>();
            _uow = new UnitOfWork();
        }

        public LiveQuizFacade(IUnitOfWork uow)
        {
            _quiz = new Dictionary<int, QuizQuestion>();
            _uow = uow;
        }

        public void Start(int quizId)
        {
            QuizQuestion firstQuestion = GetFirstQuestion(quizId);
            if (firstQuestion != null)
            {
                _quiz[quizId] = firstQuestion;
            }
        }

        public void Next(int quizId)
        {
            if (GetCurrentQuestion(quizId) != null)
            {
                QuizQuestion nextQuestion = GetNextQuestion(quizId, GetCurrentQuestion(quizId).QuizQuestionID);
                _quiz[quizId] = nextQuestion;
            }
        }

        public void Prev(int quizId)
        {
            if (GetCurrentQuestion(quizId) != null)
            {
                QuizQuestion nextQuestion = GetPrevQuestion(quizId, GetCurrentQuestion(quizId).QuizQuestionID);
                _quiz[quizId] = nextQuestion;
            }
        }

        public QuizQuestion GetCurrentQuestion(int quizId)
        {
            if(_quiz.Where(x => x.Key == quizId).Count() > 0)
            {
                return _quiz[quizId];
            }
            return null;
        }

        public int GetCurrentQuestionIndex(int quizId)
        {
            QuizQuestion currentQuestion = GetCurrentQuestion(quizId);
            if(currentQuestion == null)
            {
                return 0;
            }

            var questions = _uow.QuizQuestionRepository.GetAll().Where(x => x.QuizID == quizId).OrderBy(x => x.QuizQuestionID).ToList();

            int i = 0;
            foreach (var q in questions)
            {
                i++;
                if (q.QuizQuestionID == currentQuestion.QuizQuestionID)
                {
                    return i;
                }
            }
            return i;
        }

        public int GetTotalQuestions(int quizId)
        {
            return _uow.QuizQuestionRepository.GetAll().Where(x => x.QuizID == quizId).Count();
        }

        public void Answer(int questionId, int answer, int userId)
        {
            QuizAnswer ans = new QuizAnswer();
            ans.QuestionID = questionId;
            ans.ChoiceSelected = answer;
            ans.UserID = userId;
            ans.IsCorrect = isAnwerCorrect(questionId, answer);
            ans.ObsInd = "N";

            _uow.QuizAnswerRepository.Insert(ans);
            _uow.Save();
        }

        public string GetAnswerStatistic(int questionId)
        {
            var query = _uow.QuizChoiceRepository.GetAll().Where(x => x.QuestionID == questionId).OrderBy(x => x.QuestionID)
                .Select(x => new { x.QuizChoiceID, Count = x.QuizAnswers.Count() });

            return JsonConvert.SerializeObject(query, Formatting.None);
        }

        private bool isAnwerCorrect(int questionId, int answer)
        {
            QuizQuestion question = _uow.QuizQuestionRepository.GetById(questionId);
            if(question.CorrectAnswer == answer)
            {
                return true;
            }
            return false;
        }

        private QuizQuestion GetFirstQuestion(int quizId)
        {
            return _uow.QuizQuestionRepository.GetAll().Where(x => x.QuizID == quizId).OrderBy(x => x.QuizQuestionID).FirstOrDefault();
        }

        private QuizQuestion GetNextQuestion(int quizId, int currentQuestionId)
        {
            return _uow.QuizQuestionRepository.GetAll().Where(x => x.QuizID == quizId && x.QuizQuestionID > currentQuestionId)
                .OrderBy(x => x.QuizQuestionID).FirstOrDefault();
        }

        private QuizQuestion GetPrevQuestion(int quizId, int currentQuestionId)
        {
            return _uow.QuizQuestionRepository.GetAll().Where(x => x.QuizID == quizId && x.QuizQuestionID < currentQuestionId)
                .OrderByDescending(x => x.QuizQuestionID).FirstOrDefault();
        }
    }
}
