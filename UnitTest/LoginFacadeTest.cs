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
        private Mock<IUnitOfWork> mockUow;
        private IUnitOfWork uow;
        private LoginFacade facade;
        private List<User> users;

        [TestInitialize()]
        public void Initialize()
        {
            facade = new LoginFacade();
            users = new List<User>();

            User user1 = new User();
            user1.UserID = 1;
            user1.NRIC = "S123456";
            user1.Name = "Kevin";
            user1.Email = "test@gmail.com";
            user1.Salt = "5000.9DDz2mGPcnxLu9F10asZRg==";
            user1.Password = "2tWfT/TFnYe0fyAxokdluxwxhutqHv/pjHWi6fXGNJtZoY616zU+aW79XlN7V2f5L2RAodCsDBopyO16WteG5A==";
            user1.ObsInd = "N";

            Role role1 = new Role();
            role1.Name = "Lecturer";

            user1.Roles.Add(role1);

            users.Add(user1);            
      

            User user2 = new User();
            user2.UserID = 2;
            user2.NRIC = "S11111A";
            user2.Name = "Kelly";
            user2.Email = "kelly@gmail.com";
            user2.Salt = "5000.pNh4GxVXMV2+knnv7dhkYA==";
            user2.Password = "E1+tROaQCpQi+DmpYIqMnmIIrBZCyCxQFgArABqY27cMU99kFbp/KSLb3fH7aZ4uBKF9LMEqUSfsiZen6DaDLQ==";
            user2.ObsInd = "N";

            Role role2 = new Role();
            role2.Name = "Student";

            user2.Roles.Add(role2);

            users.Add(user2);

            User user3 = new User();
            user3.UserID = 3;
            user3.NRIC = "S11111";
            user3.Name = "silvia";
            user3.Email = "silvia@gmail.com";
            user3.Salt = "5000.RlvDsgz2ya/NHv9gzDw3+A==";
            user3.Password = "MhO5xLO+ouNJc429IXCakoECUvOgoSRJ45yitYTekW8K1dJj3rNpMTBdtRwn045hZ0fbABscHZ5lf+VYaXocJA==";
            user3.ObsInd = "N";
            users.Add(user3);

            mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(x => x.UserRepository.GetAll()).Returns(users.AsQueryable());

            uow = mockUow.Object;
            facade = new LoginFacade(uow);
        }

        [TestMethod]
        public void TestCorrectLogin()
        {
            Assert.AreEqual(users[0], facade.login("test@gmail.com", "test123"));
            Assert.AreEqual(users[1], facade.login("kelly@gmail.com", "test123"));
        }

        [TestMethod]
        public void TestWrongLogin()
        {
            Assert.IsNull(facade.login("test@gmail.com", "test1234"));
            Assert.IsNull(facade.login("jimmy@gmail.com", "test123"));
            try
            {
                facade.login("test@gmail.com", "");
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void TestGetUserRole()
        {
            mockUow.Setup(x => x.UserRepository.GetById(It.IsAny<int>()))
                .Returns(users[0]); ;
            Assert.AreEqual("Lecturer", facade.getUserRole(1));

            mockUow.Setup(x => x.UserRepository.GetById(It.IsAny<int>()))
                .Returns(users[1]); ;
            Assert.AreEqual("Student", facade.getUserRole(2));

            mockUow.Setup(x => x.UserRepository.GetById(It.IsAny<int>()))
               .Returns(users[2]); ;
            Assert.AreEqual("", facade.getUserRole(3));
        }
    }
}
