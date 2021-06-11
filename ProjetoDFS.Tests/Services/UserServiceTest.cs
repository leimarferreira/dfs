using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoDFS.Persistence.Contexts;
using ProjetoDFS.Persistence.Repositories;
using ProjetoDFS.Services;
using ProjetoDFS.Tests.Helpers;

namespace ProjetoDFS.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private static AppDbContext _dbContext { get; set; }
        private static UserService _userService { get; set; }

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            _dbContext = DbContextManager.CreateContext();
            var userRepository = new UserRepository(_dbContext);
            var unitOfWork = new UnitOfWork(_dbContext);
            _userService = new UserService(userRepository, unitOfWork);
        }

        [TestCleanup]
        public void CleanTests()
        {
            try
            {
                var users = _dbContext.Users.ToArrayAsync().Result;
                _dbContext.Users.RemoveRange(users);
                _dbContext.SaveChanges();
            }
            catch(Exception)
            {

            }
        }

        [TestMethod]
        public async Task SaveUser_ShouldSucceed()
        {
            var user = new UserBuilder().DefaultUser().Build();

            var response = await _userService.SaveAsync(user);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task SaveUser_ShouldFailIfRequiredFieldsAreMissing()
        {
            var user = new UserBuilder().Build();

            var response = await _userService.SaveAsync(user);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task SaveUser_ShouldFailIfEmailIsInvalid()
        {
            var user = new UserBuilder()
                .DefaultUser()
                .WithEmail("invalidemail")
                .Build();

            var response = await _userService.SaveAsync(user);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Invalid email.", response.Message);
        }

        [TestMethod]
        public async Task SaveUser_ShouldFailIfEmailIsAlreadyInUse()
        {
            var user = new UserBuilder().DefaultUser().Build();
            await _userService.SaveAsync(user);

            var anotheUser = new UserBuilder()
                .DefaultUser()
                .WithCpf("99145154015")
                .Build();
            var response = await _userService.SaveAsync(anotheUser);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Email already in use.", response.Message);
        }

        [TestMethod]
        public async Task SaveUser_ShouldFailIfCpfIsInvalid()
        {
            var user = new UserBuilder()
                .DefaultUser()
                .WithCpf("00000000000")
                .Build();

            var response = await _userService.SaveAsync(user);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Invalid CPF.", response.Message);
        }

        [TestMethod]
        public async Task SaveUser_ShouldFailIfCpfIsAlreadyInUse()
        {
            var user = new UserBuilder()
                .DefaultUser()
                .WithCpf("99145154015")
                .Build();

            await _userService.SaveAsync(user);
            
            var anotherUser = new UserBuilder()
                .DefaultUser()
                .WithEmail("anotheruser@domain.com")
                .WithCpf("99145154015")
                .Build();
            
            var response = await _userService.SaveAsync(anotherUser);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("CPF already in use.", response.Message);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldSucceed()
        {
            var user = new UserBuilder().DefaultUser().Build();
            
            int id = (await _userService.SaveAsync(user)).User.Id;

            user = new UserBuilder()
                .WithName("Updated Name")
                .WithEmail("updatedemail@domain.com")
                .WithCpf("26464535072")
                .WithPassword("updatedpassword")
                .Build();
            
            var response = await _userService.UpdateAsync(id, user);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldFailIfRequiredFieldsAreMissing()
        {
            var user = new UserBuilder()
                .DefaultUser()
                .Build();
            int id = (await _userService.SaveAsync(user)).User.Id;
            
            user = new UserBuilder().Build();
            var response = await _userService.UpdateAsync(id, user);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldFailIfEmailIsAlreadyInUse()
        {
            var user = new UserBuilder().DefaultUser().Build();
            int id = (await _userService.SaveAsync(user)).User.Id;

            var anotherUser = new UserBuilder()
                .DefaultUser()
                .WithEmail("anotheremail@domain.com")
                .WithCpf("63637769026")
                .Build();
            await _userService.SaveAsync(anotherUser);

            user = new UserBuilder()
                .DefaultUser()
                .WithEmail("anotheremail@domain.com")
                .Build();
            
            var response = await _userService.UpdateAsync(id, user);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Email already in use by another user.", response.Message);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldFailIfEmailIsInvalid()
        {
            var user = new UserBuilder().DefaultUser().Build();
            int id = (await _userService.SaveAsync(user)).User.Id;

            user = new UserBuilder()
                .DefaultUser()
                .WithEmail("invalidemail")
                .Build();
            
            var response = await _userService.UpdateAsync(id, user);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Invalid email.", response.Message);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldFailIfCpfIsAlreadyInUse()
        {
            var user = new UserBuilder().DefaultUser().Build();
            int id = (await _userService.SaveAsync(user)).User.Id;

            var anotherUser = new UserBuilder()
                .DefaultUser()
                .WithEmail("anotheremail@domain.com")
                .WithCpf("30738104043")
                .Build();
            await _userService.SaveAsync(anotherUser);

            user = new UserBuilder()
                .DefaultUser()
                .WithCpf("30738104043")
                .Build();

            var response = await _userService.UpdateAsync(id, user);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("CPF already in use by another user.", response.Message);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldFailIfCpfIsInvalid()
        {
            var user = new UserBuilder().DefaultUser().Build();
            int id = (await _userService.SaveAsync(user)).User.Id;

            user = new UserBuilder()
                .DefaultUser()
                .WithCpf("00000000000")
                .Build();

            var response = await _userService.UpdateAsync(id, user);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Invalid CPF.", response.Message);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldFailIfUserDoesntExist()
        {
            var user = new UserBuilder().DefaultUser().Build();
            var response = await _userService.UpdateAsync(100000, user);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("User not found.", response.Message);
        }

        [TestMethod]
        public async Task DeleteUser_ShouldSucceed()
        {
            var user = new UserBuilder().DefaultUser().Build();
            int id = (await _userService.SaveAsync(user)).User.Id;
            var response = await _userService.DeleteAsync(id);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task DeleteUser_ShouldFailIfUserDoesntExist()
        {
            var response = await _userService.DeleteAsync(10000);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("User not found.", response.Message);
        }
    }
}
