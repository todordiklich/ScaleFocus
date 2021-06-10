using Moq;
using RateApp.BLL.Services;
using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RateApp.BLL.UnitTests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUser_ValidUsername_ReturnsTrue()
        {
            // arrange
            var userRepositoryStub = new Mock<IUserManager>();
            var sut = new UserService(userRepositoryStub.Object);

            // act
            var result = await sut.CreateUser("fake-user-name", "password");

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateUser_ExistingUsername_ReturnsFalse()
        {
            // arrange
            var userRepositoryStub = new Mock<IUserManager>();
            userRepositoryStub.Setup(userService => userService.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());
            var sut = new UserService(userRepositoryStub.Object);

            // act
            var result = await sut.CreateUser("fake-user-name","password");

            // assert
            Assert.False(result);
        }

        [Fact]
        public async Task CreateUser_ValidUsername_CallsCreateOrUpdate()
        {
            // arrange
            var userRepositoryMock = new Mock<IUserManager>();
            var sut = new UserService(userRepositoryMock.Object);

            // act
            var result = await sut.CreateUser("fake-user-name", "password");

            // assert
            userRepositoryMock.Verify(mock => 
                mock.CreateUserAsync(It.Is<User>(u => u.UserName == "fake-user-name"), It.IsAny<string>()), 
                Times.Once);
        }

        [Fact]
        public async Task GetAll_Default_ShouldReturnRepositoryCollection()
        {
            // arrange
            var users = new List<User>
            {
                new User { UserName = "user-1" },
                new User { UserName = "user-2" },
            };
            var userRepositoryStub = new Mock<IUserManager>();
            userRepositoryStub.Setup(userRepositoryStub => userRepositoryStub.GetAllAsync())
                .ReturnsAsync(users);
            var sut = new UserService(userRepositoryStub.Object);

            // act
            var result = await sut.GetAll();

            // assert
            Assert.Equal(users, result);
        }
    }
}
