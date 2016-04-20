using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BLL.Facade;
using DL;
using Newtonsoft.Json;
using DL.ModelView;
using JLearnWeb.Extensions;

namespace JLearnWeb
{
    public class QuizHub : Hub
    {
        public static LiveQuizFacade _quiz = new LiveQuizFacade();

        public void Start(int quizId)
        {
            _quiz.Start(quizId);
            QuizQuestion firstQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(firstQuestion);

            Clients.All.setQuestion(quizId, JsonConvert.SerializeObject(question));
            Clients.All.setStats(quizId, _quiz.GetAnswerStatistic(question.QuestionID));
        }

        public void Next(int quizId)
        {
            _quiz.Next(quizId);
            QuizQuestion currentQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(currentQuestion);

            Clients.All.setQuestion(quizId, JsonConvert.SerializeObject(question));
            Clients.All.setStats(quizId, _quiz.GetAnswerStatistic(question.QuestionID));
        }

        public void Prev(int quizId)
        {
            _quiz.Prev(quizId);
            QuizQuestion currentQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(currentQuestion);

            Clients.All.setQuestion(quizId, JsonConvert.SerializeObject(question));
            Clients.All.setStats(quizId, _quiz.GetAnswerStatistic(question.QuestionID));
        }

        public void GetCurrentQuestion(int quizId)
        {
            QuizQuestion currentQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(currentQuestion);

            Clients.Caller.setQuestion(quizId, JsonConvert.SerializeObject(question));
        }

        public void Answer(int questionId, int answer, int userId, int quizId)
        {
            try
            {
                _quiz.Answer(questionId, answer, userId);
                Clients.Caller.notify(NotificationType.SUCCESS, "Answer submitted");
                Clients.All.setStats(quizId, _quiz.GetAnswerStatistic(questionId));
            }
            catch (Exception ex)
            {
                Clients.Caller.notify(NotificationType.ERROR, ex.Message);
            }        
        }

        private LiveQuizModelView MapQuizToViewModel(QuizQuestion question)
        {
            if(question == null)
            {
                return null;
            }

            LiveQuizModelView questionModel = new LiveQuizModelView();
            questionModel.QuestionID = question.QuizQuestionID;
            questionModel.Title = question.Title;
            foreach (var c in question.QuizChoices.OrderBy(x => x.QuizChoiceID))
            {
                QuizChoiceModelView choice = new QuizChoiceModelView();
                choice.ChoiceID = c.QuizChoiceID;
                choice.Choice = c.Choice;
                questionModel.Choice.Add(choice);
            }
            questionModel.TotalQuestions = _quiz.GetTotalQuestions(question.Quiz.QuizID);
            questionModel.CurrentQuestionIndex = _quiz.GetCurrentQuestionIndex(question.Quiz.QuizID);

            return questionModel;
        }
    }
}