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
using log4net;

namespace JLearnWeb
{
    [Authorize]
    public class QuizHub : Hub
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(QuizHub));
        public static LiveQuizFacade _quiz = new LiveQuizFacade();

        public void Start(int quizId)
        {
            log.Debug("Initiate live quiz start, quizId: " + quizId);

            _quiz.Start(quizId);
            QuizQuestion firstQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(firstQuestion);

            Clients.All.setQuestion(quizId, JsonConvert.SerializeObject(question));
            log.Debug("Broadcast question: " + JsonConvert.SerializeObject(question));

            string stats = _quiz.GetAnswerStatistic(question.QuestionID);
            Clients.All.setStats(quizId, stats);
            log.Debug("Broadcast answer statistic: " + stats);
        }

        public void Next(int quizId)
        {
            log.Debug("Trigger next question: " + quizId);

            _quiz.Next(quizId);
            QuizQuestion currentQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(currentQuestion);

            Clients.All.setQuestion(quizId, JsonConvert.SerializeObject(question));
            log.Debug("Broadcast question: " + JsonConvert.SerializeObject(question));


            string stats = _quiz.GetAnswerStatistic(question.QuestionID);
            Clients.All.setStats(quizId, stats);
            log.Debug("Broadcast answer statistic: " + stats);
        }

        public void Prev(int quizId)
        {
            log.Debug("Trigger previous question: " + quizId);

            _quiz.Prev(quizId);
            QuizQuestion currentQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(currentQuestion);

            Clients.All.setQuestion(quizId, JsonConvert.SerializeObject(question));
            log.Debug("Broadcast question: " + JsonConvert.SerializeObject(question));

            string stats = _quiz.GetAnswerStatistic(question.QuestionID);
            Clients.All.setStats(quizId, stats);
            log.Debug("Broadcast answer statistic: " + stats);
        }

        public void GetCurrentQuestion(int quizId)
        {
            log.Debug("Curent question request quizId: " + quizId);
            QuizQuestion currentQuestion = _quiz.GetCurrentQuestion(quizId);

            LiveQuizModelView question = MapQuizToViewModel(currentQuestion);

            Clients.Caller.setQuestion(quizId, JsonConvert.SerializeObject(question));
            log.Debug("Broadcast question: " + JsonConvert.SerializeObject(question));
        }

        public void Answer(int questionId, int answer, int userId, int quizId)
        {
            try
            {
                log.Debug("Question answer questionId: " + questionId + " answer: " + answer + " userId: " + userId + " quizId: " + quizId);

                _quiz.Answer(questionId, answer, userId);
                log.Debug("Answer saved to database");

                Clients.Caller.notify(NotificationType.SUCCESS, "Answer submitted");
                log.Debug("Notify answer submitted");

                string stats = _quiz.GetAnswerStatistic(questionId);
                Clients.All.setStats(quizId, stats);
                log.Debug("Broadcast answer statistic: " + stats);
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
                Clients.Caller.notify(NotificationType.ERROR, ex.Message);
                log.Debug("Notify error");
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