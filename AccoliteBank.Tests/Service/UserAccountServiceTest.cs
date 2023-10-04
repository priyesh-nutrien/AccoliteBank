using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccoliteBank.DataAccess.IRepository;
using AccoliteBank.Models;
using AccoliteBank.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccoliteBank.Tests.Service
{
    [TestClass]

    public class UserAccountServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UserAccountServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [TestMethod]
        public async Task CreateAccount_Success()
        {
            // Arrange        
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.AddAsync(It.IsAny<UserAccount>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            var response = await userAccountService.CreateAccount(new UserAccount() { Id = It.IsAny<int>(), Name = It.IsAny<string>(), Balance = 100, UserId = It.IsAny<int>(), AccountTypeId = It.IsAny<int>() });

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CreateAccount_Fail_UserNotExist()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync((User)null);
            _unitOfWorkMock.Setup(x => x.UserAccount.AddAsync(It.IsAny<UserAccount>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            async Task CreateAccountTest() => await userAccountService.CreateAccount(new UserAccount() { Id = It.IsAny<int>(), Name = It.IsAny<string>(), Balance = 100, UserId = It.IsAny<int>(), AccountTypeId = It.IsAny<int>() });

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<MyCustomException>(() => CreateAccountTest());

            Assert.AreEqual("User not exist!", exception.Message);
        }

        [TestMethod]
        public async Task CreateAccount_Fail_BalanceNoLessThan100()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.AddAsync(It.IsAny<UserAccount>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            async Task CreateAccountTest() => await userAccountService.CreateAccount(new UserAccount() { Id = It.IsAny<int>(), Name = It.IsAny<string>(), Balance = 99, UserId = It.IsAny<int>(), AccountTypeId = It.IsAny<int>() });

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<MyCustomException>(() => CreateAccountTest());

            Assert.AreEqual("An account cannot have less than $100 at any time in an account.", exception.Message);
        }


        [TestMethod]
        public async Task DeleteAccount_Success()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.GetAsync(It.IsAny<int>())).ReturnsAsync(new UserAccount());
            _unitOfWorkMock.Setup(x => x.UserAccount.RemoveAsync(It.IsAny<UserAccount>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            var response = await userAccountService.DeleteAccount(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(Response));
            Assert.AreEqual(response.Message, "Deleted Successfully!");
        }

        [TestMethod]
        public async Task DeleteAccount_Fail_AccountNotExist()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.GetAsync(It.IsAny<int>())).ReturnsAsync((UserAccount)null);
            _unitOfWorkMock.Setup(x => x.UserAccount.RemoveAsync(It.IsAny<UserAccount>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            async Task DeleteAccountTest() => await userAccountService.DeleteAccount(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<MyCustomException>(() => DeleteAccountTest());

            Assert.AreEqual("Account not exist!", exception.Message);
        }

        [TestMethod]
        public async Task Deposit_Success()
        {
            var userAccount = new UserAccount
            {
                Id = 1,
                AccountTypeId = 1,
                UserId = 1,
                Name = "Personal 1",
                CreatedDate = DateTime.Now,
                Balance = 10000.67m
            };
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.GetAsync(It.IsAny<int>())).ReturnsAsync(userAccount);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            var response = await userAccountService.Deposit(1, 1, 100);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(decimal));
            Assert.AreEqual(response, 10100.67m);
        }

        [TestMethod]
        public async Task Deposit_Fail_DepostOver10000()
        {
            var userAccount = new UserAccount
            {
                Id = 1,
                AccountTypeId = 1,
                UserId = 1,
                Name = "Personal 1",
                CreatedDate = DateTime.Now,
                Balance = 10000.67m
            };
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.GetAsync(It.IsAny<int>())).ReturnsAsync(userAccount);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            async Task DepositTest() => await userAccountService.Deposit(1, 1, 10001);

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<MyCustomException>(() => DepositTest());

            Assert.AreEqual("A user cannot deposit more than $10,000 in a single transaction.", exception.Message);
        }

        [TestMethod]
        public async Task Withdraw_Success()
        {
            var userAccount = new UserAccount
            {
                Id = 1,
                AccountTypeId = 1,
                UserId = 1,
                Name = "Personal 1",
                CreatedDate = DateTime.Now,
                Balance = 10000.67m
            };
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.GetAsync(It.IsAny<int>())).ReturnsAsync(userAccount);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            var response = await userAccountService.Withdraw(1, 1, 1000);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(decimal));
            Assert.AreEqual(response, 9000.67m);
        }

        [TestMethod]
        public async Task Withdraw_Fail_WithdrawMoreThan90Per()
        {
            var userAccount = new UserAccount
            {
                Id = 1,
                AccountTypeId = 1,
                UserId = 1,
                Name = "Personal 1",
                CreatedDate = DateTime.Now,
                Balance = 10000.67m
            };
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.GetAsync(It.IsAny<int>())).ReturnsAsync(userAccount);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            async Task WithdrawTest() => await userAccountService.Withdraw(1, 1, 9005);

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<MyCustomException>(() => WithdrawTest());

            Assert.AreEqual("A user cannot withdraw more than 90% of their total balance from an account in a single transaction.", exception.Message);
        }

        [TestMethod]
        public async Task Withdraw_Fail_CannotHaveLessThan100Balance()
        {
            var userAccount = new UserAccount
            {
                Id = 1,
                AccountTypeId = 1,
                UserId = 1,
                Name = "Personal 1",
                CreatedDate = DateTime.Now,
                Balance = 10000.67m
            };
            // Arrange
            _unitOfWorkMock.Setup(x => x.User.GetAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _unitOfWorkMock.Setup(x => x.UserAccount.GetAsync(It.IsAny<int>())).ReturnsAsync(userAccount);
            var userAccountService = new UserAccountService(_unitOfWorkMock.Object);

            // Act
            async Task WithdrawTest() => await userAccountService.Withdraw(1, 1, 9990);

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<MyCustomException>(() => WithdrawTest());

            Assert.AreEqual("An account cannot have less than $100 at any time in an account.", exception.Message);
        }
    }
}
