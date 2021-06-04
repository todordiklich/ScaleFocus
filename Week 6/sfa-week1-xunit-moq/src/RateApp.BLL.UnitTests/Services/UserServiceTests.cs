using Moq;
using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RateApp.BLL.UnitTests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void CreateUser_ValidUsername_ReturnsTrue()
        {
            // arrange
            var userRepositoryStub = new Mock<IRepository<User>>();
            var sut = new UserService(userRepositoryStub.Object);

            // act
            var result = sut.CreateUser("fake-user-name");

            // assert
            Assert.True(result);
        }

        [Fact]
        public void CreateUser_ExistingUsername_ReturnsFalse()
        {
            // arrange
            var userRepositoryStub = new Mock<IRepository<User>>();
            userRepositoryStub.Setup(userRepository => userRepository.Get(It.IsAny<Func<User, bool>>()))
                .Returns(new User());
            var sut = new UserService(userRepositoryStub.Object);

            // act
            var result = sut.CreateUser("fake-user-name");

            // assert
            Assert.False(result);
        }

        [Fact]
        public void CreateUser_ValidUsername_CallsCreateOrUpdate()
        {
            // arrange
            var userRepositoryMock = new Mock<IRepository<User>>();
            var sut = new UserService(userRepositoryMock.Object);

            // act
            var result = sut.CreateUser("fake-user-name");

            // assert
            userRepositoryMock.Verify(mock => 
                mock.CreateOrUpdate(It.Is<User>(u => u.Name == "fake-user-name")), 
                Times.Once);
        }

        [Fact]
        public void GetAll_Default_ShouldReturnRepositoryCollection()
        {
            // arrange
            var users = new List<User>
            {
                new User { Name = "user-1" },
                new User { Name = "user-2" },
            };
            var userRepositoryStub = new Mock<IRepository<User>>();
            userRepositoryStub.Setup(userRepo => userRepo.All())
                .Returns(users);
            var sut = new UserService(userRepositoryStub.Object);

            // act
            var result = sut.GetAll();

            // assert
            Assert.Equal(users, result);
        }
    }
}
