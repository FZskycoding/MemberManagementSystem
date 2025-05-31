using Xunit;
using MemberManagementSystem.Models;
using MemberManagementSystem.Repositories;
using MemberManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace MemberManagementSystem.Tests
{
    public class UserRepositoryTests
    {
        //立一個「只存在記憶體中」的資料庫給單元測試使用
        private AppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void GetByEmail_ShouldReturnUser_WhenEmailExists()
        {
            // Arrange
            var db = GetInMemoryDb();
            db.Users.Add(new User { Name = "Test", Email = "test@example.com", Password = "123" });
            db.SaveChanges();

            var repo = new UserRepository(db);

            // Act
            var user = repo.GetByEmail("test@example.com");

            // Assert
            Assert.NotNull(user);
            Assert.Equal("Test", user.Name);
        }
    }
}
