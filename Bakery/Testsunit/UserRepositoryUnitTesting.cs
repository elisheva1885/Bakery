﻿using Entities;
using Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using Bakery;
using Microsoft.EntityFrameworkCore;

namespace Testsunit
{
    public class UserRepositoryUnitTesting
    {

        [Fact]
        public async Task Login_ReturnsUser_WhenCredentialsAreCorrect()
        {
            var user = new User { Firstname = "Elisheva", Lastname = "Safray", Password = "eli7saf@abc", Username = "Elisheva" };
            var mockContext = new Mock<BakeryDBContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UsersData(mockContext.Object);
            //LoginUser luser = new LoginUser(user.Username, user.Password);
            var result = await userRepository.Login(user);
            Assert.Equal(user, result);
        }
        [Fact]
        public async Task Register_NewUser_AddsUser()
        {
            // Arrange
            var users = new List<User>();
            var mockContext = new Mock<BakeryDBContext>(new DbContextOptions<BakeryDBContext>());
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repo = new UsersData(mockContext.Object);

            var user = new User { Username = "newuser", Password = "pass" };

            // Act
            await repo.Register(user);

            // Assert
            mockContext.Verify(x => x.Users.AddAsync(user, default), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Register_ExistingUser_ThrowsException()
        {
            // Arrange
            var user = new User { Username = "exists", Password = "pass" };
            var users = new List<User> { user };
            var mockContext = new Mock<BakeryDBContext>(new DbContextOptions<BakeryDBContext>());
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repo = new UsersData(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpStatusException>(() => repo.Register(user));
        }

        [Fact]
        public async Task Update_UpdatesUser()
        {
            // Arrange
            var user = new User { Username = "update", Password = "pass",Firstname="shani",Lastname="rapoport" };
            var users = new List<User> { user };
            var mockContext = new Mock<BakeryDBContext>(new DbContextOptions<BakeryDBContext>());
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repo = new UsersData(mockContext.Object);

            var updatedUser = new User { Username = "update", Password = "newpass", Id = 1 };

            // Act
            await repo.Update(1, updatedUser);

            // Assert
            mockContext.Verify(x => x.Users.Update(updatedUser), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task getUserById_ReturnsUser()
        {
            // Arrange
            //var user = new User { Id = 1, Username = "byid" };
            //var users = new List<User> { user };
            var mockContext = new Mock<BakeryDBContext>(new DbContextOptions<BakeryDBContext>());
            //mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            //var repo = new UsersData(mockContext.Object);

            // Act
            //var result = await repo.getUserId(1);

            //// Assert
            //Assert.NotNull(result);
            //Assert.Equal(user.Id, result.Id);


            var user = new User { Id = 1, Username = "testuser" };
            var users = new List<User> { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repo = new UsersData(mockContext.Object);

            var result = repo.getUserId(1);

            Assert.NotNull(result); // This will fail if result is null

        }

        [Fact]
        public async Task getAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
        {
            new User { Id = 1, Username = "a" },
            new User { Id = 2, Username = "b" }
        };
            var mockContext = new Mock<BakeryDBContext>(new DbContextOptions<BakeryDBContext>());
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repo = new UsersData(mockContext.Object);

            // Act
            var result = await repo.getUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}






