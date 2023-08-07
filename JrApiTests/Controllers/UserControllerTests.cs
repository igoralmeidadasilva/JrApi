using Microsoft.VisualStudio.TestTools.UnitTesting;
using JrApi.Data;
using JrApi.Models;
using JrApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc;

namespace JrApi.Controllers.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserDbContext? _context;
        private UserRepository? _itemRepository;
        private DbConnection? _connection;
        
        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new UserDbContext(options);
            _context.Database.EnsureCreated();

            _itemRepository = new UserRepository(_context);

            _context.Users.Add(new UserModel("TestName1", "LastName", DateTime.Now));
            _context.Users.Add(new UserModel("TestName1", "LastName", DateTime.Now));
            _context.Users.Add(new UserModel("TestName3", "LastName", DateTime.Now));
            _context.Users.Add(new UserModel("TestName4", "LastName", DateTime.Now));
            _context.Users.Add(new UserModel("TestName5", "LastName", DateTime.Now));
            _context.SaveChanges();
            
        }

        [TestCleanup]
        public void TearDown()
        {
            _context!.Database.EnsureDeleted();
            _connection!.Close();
        }


        [TestMethod]
        public async Task SelectAllUsersTest()
        {
            // Arrange
            var controller = new UserController(_itemRepository!);
            
            // Act
            var result = await controller.SelectAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Value?.Count);

        }
        
        [TestMethod]
        public async Task SelectUserByIdTest()
        {
            // Arrange
            var controller = new UserController(_itemRepository!);
            int id = 1;

            // Act
            var result = await controller.SelectUserById(id);
            var objectResult = result.Result as OkObjectResult;
            var itemResult = objectResult?.Value as UserModel;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, itemResult?.Id); 
        }

        [TestMethod]
        public async Task InsertTest()
        {
            // Arrange
            var controller = new UserController(_itemRepository!);
            var newItem = new UserModel(6, "TestName6", "LastName", DateTime.Now);

            // Act
            var result = await controller.Insert(newItem);
            var objectResult = result.Result as CreatedResult;
            var itemResult = objectResult?.Value as UserModel;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newItem, itemResult);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            // Arrange
            var controller = new UserController(_itemRepository!);
            var id = 1;
            var updateItem = new UserModel(id, "TestNameX", "LastNameX", DateTime.Now);

            // Act
            var result = await controller.Update(updateItem, id);
            var objectResult = result.Result as OkObjectResult;
            var itemResult = objectResult?.Value as UserModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateItem.Id, itemResult?.Id);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            var controller = new UserController(_itemRepository!);
            var id = 1;

            // Act
            var result = await controller.Delete(id);
            var objectResult = result.Result as OkObjectResult;
            var itemResult = objectResult?.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, itemResult);
        }
    }
}

