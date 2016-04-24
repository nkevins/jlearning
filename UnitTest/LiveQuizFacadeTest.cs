using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Repository;
using BLL.Facade;
using Moq;
using System.Collections.Generic;
using DL;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class LiveQuizFacadeTest
    {
        private Mock<IUnitOfWork> mockUow;
        private IUnitOfWork uow;
        private LiveQuizFacade facade;
        private List<QuizQuestion> questions;
        private QuizAnswer answer;

        [TestInitialize()]
        public void Initialize()
        {
            // Setup question data
            questions = new List<QuizQuestion>();
            for (int i = 1; i <= 3; i++)
            {
                QuizQuestion question = new QuizQuestion();
                question.QuizQuestionID = i;
                question.QuizID = 1;
                question.Title = "Quiz" + i.ToString();
                question.CorrectAnswer = 3;
                questions.Add(question);
            }

            mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(x => x.QuizQuestionRepository.GetAll()).Returns(questions.AsQueryable());
            mockUow.Setup(x => x.QuizAnswerRepository.Insert(It.IsAny<QuizAnswer>()))
                .Callback<QuizAnswer>(x => answer = x);

            uow = mockUow.Object;
            facade = new LiveQuizFacade(uow);
    }

        [TestMethod]
        public void TestStart()
        {
            Assert.IsNull(facade.GetCurrentQuestion(1));
            Assert.AreEqual(0, facade.GetCurrentQuestionIndex(1));
            facade.Start(1);
            Assert.AreEqual(questions[0], facade.GetCurrentQuestion(1));
            Assert.AreEqual(1, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
        }

        [TestMethod]
        public void TestStartWithNoQuestion()
        {
            questions = new List<QuizQuestion>();
            mockUow.Setup(x => x.QuizQuestionRepository.GetAll()).Returns(questions.AsQueryable());

            Assert.IsNull(facade.GetCurrentQuestion(1));
            Assert.AreEqual(0, facade.GetCurrentQuestionIndex(1));
            facade.Start(1);
            Assert.IsNull(facade.GetCurrentQuestion(1));
            Assert.AreEqual(0, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(0, facade.GetTotalQuestions(1));
        }

        [TestMethod]
        public void TestNext()
        {
            facade.Start(1);
            facade.Next(1);
            Assert.AreEqual(questions[1], facade.GetCurrentQuestion(1));
            Assert.AreEqual(2, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
            facade.Next(1);
            Assert.AreEqual(questions[2], facade.GetCurrentQuestion(1));
            Assert.AreEqual(3, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
            facade.Next(1);
            Assert.AreEqual(questions[2], facade.GetCurrentQuestion(1));
            Assert.AreEqual(3, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
        }

        [TestMethod]
        public void TestPrev()
        {
            facade.Start(1);
            facade.Next(1);
            facade.Next(1);
            Assert.AreEqual(questions[2], facade.GetCurrentQuestion(1));
            Assert.AreEqual(3, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
            facade.Prev(1);
            Assert.AreEqual(questions[1], facade.GetCurrentQuestion(1));
            Assert.AreEqual(2, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
            facade.Prev(1);
            Assert.AreEqual(questions[0], facade.GetCurrentQuestion(1));
            Assert.AreEqual(1, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
            facade.Prev(1);
            Assert.AreEqual(questions[0], facade.GetCurrentQuestion(1));
            Assert.AreEqual(1, facade.GetCurrentQuestionIndex(1));
            Assert.AreEqual(3, facade.GetTotalQuestions(1));
        }

        [TestMethod]
        public void TestCorrectAnswer()
        {
            facade.Start(1);
            mockUow.Setup(x => x.QuizQuestionRepository.GetById(1))
               .Returns(questions[0]);
            facade.Answer(1, 3, 1);
            // Verify that record is inserted into DB
            mockUow.Verify(x => x.QuizAnswerRepository.Insert(It.IsAny<QuizAnswer>()), Times.Once);
            // Verify that answer is marked as correct
            Assert.AreEqual(true, answer.IsCorrect);
        }

        [TestMethod]
        public void TestWrongAnswer()
        {
            facade.Start(1);
            mockUow.Setup(x => x.QuizQuestionRepository.GetById(1))
               .Returns(questions[0]);
            facade.Answer(1, 2, 1);
            // Verify that record is inserted into DB
            mockUow.Verify(x => x.QuizAnswerRepository.Insert(It.IsAny<QuizAnswer>()), Times.Once);
            // Verify that answer is marked as correct
            Assert.AreEqual(false, answer.IsCorrect);
        }

        [TestMethod]
        public void TestAnswerStatistic()
        {
            // Prepare data
            List<QuizChoice> choices = new List<QuizChoice>();
           
            QuizChoice choice1 = new QuizChoice();
            choice1.QuizChoiceID = 1;
            choice1.Choice = "A";
            choice1.QuestionID = 1;

            QuizChoice choice2 = new QuizChoice();
            choice2.QuizChoiceID = 2;
            choice2.Choice = "B";
            choice2.QuestionID = 1;
            choice2.QuizAnswers.Add(new QuizAnswer());
            choice2.QuizAnswers.Add(new QuizAnswer());

            choices.Add(choice1);
            choices.Add(choice2);

            mockUow.Setup(x => x.QuizChoiceRepository.GetAll()).Returns(choices.AsQueryable());

            // Test
            string jsonResult = facade.GetAnswerStatistic(1);
            Assert.AreEqual("[{\"QuizChoiceID\":1,\"Count\":0},{\"QuizChoiceID\":2,\"Count\":2}]", jsonResult);
        }
    }
}
