using BLL.Facade;
using DAL.Repository;
using DL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
   public class LoginFacadeTest
    {
        private Mock<UnitOfWork> mockUow;
        private UnitOfWork uow;
        private LoginFacade facade;
        private List<User> questions;
        private User usr;

        [TestInitialize()]
        public void Initialize()
        {
            facade = new LoginFacade();
            questions = new List<User>();
            for (int i = 1; i <= 3; i++)
            {
                User question = new User();
                question.UserID = 1;
                question.NRIC = "S123456";
                question.Name = "Kevin";
                question.Email = "test@gmail.com";
                question.Salt = "5000.9DDz2mGPcnxLu9F10asZRg==";
                question.Password = "2tWfT/TFnYe0fyAxokdluxwxhutqHv/pjHWi6fXGNJtZoY616zU+aW79XlN7V2f5L2RAodCsDBopyO16WteG5A==";
                question.ObsInd = "N";
                questions.Add(question);

                question = new User();
                question.UserID = 1;
                question.NRIC = "S11111A";
                question.Name = "Kelly";
                question.Email = "kelly@gmail.com";
                question.Salt = "5000.pNh4GxVXMV2+knnv7dhkYA==";
                question.Password = "E1+tROaQCpQi+DmpYIqMnmIIrBZCyCxQFgArABqY27cMU99kFbp/KSLb3fH7aZ4uBKF9LMEqUSfsiZen6DaDLQ==";
                question.ObsInd = "N";
                questions.Add(question);

            }

            mockUow = new Mock<UnitOfWork>();
            mockUow.Setup(x => x.UserRepository.GetAll()).Returns(questions.AsQueryable());
            mockUow.Setup(x => x.UserRepository.Insert(It.IsAny<User>()))
                .Callback<User>(x => usr = x);

            uow = mockUow.Object;
            facade = new LoginFacade(uow);
        }

        [TestMethod]
        public void TestCompareUsrPassword()
        {
           
            string password = "test123";
            string userPassword = "2tWfT/TFnYe0fyAxokdluxwxhutqHv/pjHWi6fXGNJtZoY616zU+aW79XlN7V2f5L2RAodCsDBopyO16WteG5A==";
            string salt = "5000.9DDz2mGPcnxLu9F10asZRg==";
            string userInputPwd = facade.computePassword(password, salt);
            Assert.AreEqual(true,facade.compareUsrPassword(userInputPwd, userPassword));
        }
    }
}
