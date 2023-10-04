using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AccoliteBank.Services.IServices;
using AccoliteBank.Controllers;
using AccoliteBank.Models;
using AccoliteBank.DataAccess.Data;

namespace AccoliteBank.Tests.ApiController
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly Mock<IUserAccountService> _userAccountSerivceMock;
        private readonly Mock<ApplicationDbContext> _dbMock;

        public UserControllerTest()
        {
            _userAccountSerivceMock = new Mock<IUserAccountService>();
            _dbMock = new Mock<ApplicationDbContext>();
        }

        [TestMethod]
        public async Task UserController_GetAccounts_Success()
        {
            // Arrange
            _userAccountSerivceMock.Setup(x => x.GetAllAccountByUser(It.IsAny<int>())).ReturnsAsync(UserAccountsTestData);

            // Act
            var controller = new UserController(_userAccountSerivceMock.Object);

            var resp = await controller.GetAccounts(It.IsAny<int>()) as IEnumerable<UserAccount>;

            Assert.IsNotNull(resp);
            Assert.IsInstanceOfType(resp, typeof(IEnumerable<UserAccount>));
        }

        [TestMethod]
        public async Task UserController_CreateAccount_Success()
        {
            // Arrange           
            _userAccountSerivceMock.Setup(x => x.CreateAccount(It.IsAny<UserAccount>())).ReturnsAsync(new UserAccount() { Id = It.IsAny<int>(), Name = It.IsAny<string>(), Balance = 100, UserId = It.IsAny<int>(), AccountTypeId = It.IsAny<int>() });
            _dbMock.Setup(x => x.Set<User>().FindAsync(It.IsAny<int>())).ReturnsAsync(new User() { Id = It.IsAny<int>() });

            // Act
            var controller = new UserController(_userAccountSerivceMock.Object);

            var resp = await controller.CreateAccount(It.IsAny<int>(), new CreateUserAccount() { Name = It.IsAny<string>() }) as ObjectResult;

            // Assert
            Assert.IsNotNull(resp);
            Assert.AreEqual(resp.StatusCode, 200);
        }

        [TestMethod]
        public async Task UserController_CreateAccount_Fail_BalanceNoLessThan100()
        {
            // Arrange           
            _userAccountSerivceMock.Setup(x => x.CreateAccount(It.IsAny<UserAccount>())).ThrowsAsync(new Exception("An account cannot have less than $100 at any time in an account."));

            // Act
            var controller = new UserController(_userAccountSerivceMock.Object);

            var resp = await controller.CreateAccount(It.IsAny<int>(), new CreateUserAccount() { Name = It.IsAny<string>(), AccountTypeId = 2, Balance = 99 }) as ObjectResult;

            // Assert
            Assert.IsNotNull(resp);
            Assert.AreEqual(resp.StatusCode, 500);
            Assert.IsNotNull(resp.Value);
            Assert.AreEqual(resp.Value, "An account cannot have less than $100 at any time in an account.");

        }

        [TestMethod]
        public async Task UserController_DeleteAccount_Success()
        {
            // Arrange           
            _userAccountSerivceMock.Setup(x => x.DeleteAccount(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response() { Success = true, Message = "Deleted Successfully!" });

            // Act
            var controller = new UserController(_userAccountSerivceMock.Object);

            var resp = await controller.DeleteAccount(It.IsAny<int>(), It.IsAny<int>()) as ObjectResult;

            // Assert
            Assert.IsNotNull(resp);
            Assert.AreEqual(resp.StatusCode, 200);
        }

        [TestMethod]
        public async Task UserController_Withdraw_Fail_InvalidTransaction()
        {
            // Arrange           
            _userAccountSerivceMock.Setup(x => x.Withdraw(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>())).ReturnsAsync(It.IsAny<int>());

            // Act
            var controller = new UserController(_userAccountSerivceMock.Object);
            controller.ModelState.AddModelError("Amount", "Value must be greater than 0.");

            var resp = await controller.Withdraw(It.IsAny<int>(), It.IsAny<int>(), new Transaction() { Amount = 0 }) as ObjectResult;

            // Assert
            Assert.IsNotNull(resp);
            Assert.AreEqual(resp.StatusCode, 500);
            Assert.IsNotNull(resp.Value);
            Assert.AreEqual(resp.Value, "Value must be greater than 0.");
        }


        public List<UserAccount> UserAccountsTestData()
        {
            return new List<UserAccount>(){
                new UserAccount
                {
                    Id = 1,
                    AccountTypeId = 1,
                    UserId = 1,
                    Name = "Personal 1",
                    CreatedDate = DateTime.Now,
                    Balance = 10000.67m
                },
                new UserAccount
                {
                    Id = 2,
                    AccountTypeId = 1,
                    UserId = 1,
                    Name = "Personal 2",
                    CreatedDate = DateTime.Now,
                    Balance = 122.44m
                },
                new UserAccount
                {
                    Id = 3,
                    AccountTypeId = 1,
                    UserId = 1,
                    Name = "Personal 3",
                    CreatedDate = DateTime.Now,
                    Balance = 500.99m
                },
                new UserAccount
                {
                    Id = 4,
                    AccountTypeId = 2,
                    UserId = 1,
                    Name = "Saving 2",
                    CreatedDate = DateTime.Now,
                    Balance = 600.28m
                },
                new UserAccount
                {
                    Id = 5,
                    AccountTypeId = 1,
                    UserId = 2,
                    Name = "Personal 1",
                    CreatedDate = DateTime.Now,
                    Balance = 15000.62m
                },
                new UserAccount
                {
                    Id = 6,
                    AccountTypeId = 1,
                    UserId = 2,
                    Name = "Personal 2",
                    CreatedDate = DateTime.Now,
                    Balance = 1500.42m
                },
                new UserAccount
                {
                    Id = 7,
                    AccountTypeId = 2,
                    UserId = 2,
                    Name = "Saving 1",
                    CreatedDate = DateTime.Now,
                    Balance = 5500.32m
                },
                new UserAccount
                {
                    Id = 8,
                    AccountTypeId = 2,
                    UserId = 2,
                    Name = "Saving 2",
                    CreatedDate = DateTime.Now,
                    Balance = 5600.23m
                }
            };
        }

    }
}
