using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RateApp.BLL.UnitTests
{
    public class UserServiceTests
    {
        [Fact]
        public void CreateUser_ValidUsername_ReturnsTrue()
        {
            // arrange
            var userRepositoryStub = new UserRepositoryFake();
            var sut = new UserService(userRepositoryStub);

            // act
            var result = sut.CreateUser("fake-user-name");

            // assert
            Assert.True(result);
        }

        [Fact]
        public void CreateUser_ExistingUsername_ReturnsFalse()
        {
            // arrange
            var userRepositoryStub = new UserRepositoryFake();
            userRepositoryStub.GetMethodResult = new User();
            var sut = new UserService(userRepositoryStub);

            // act
            var result = sut.CreateUser("fake-user-name");

            // assert
            Assert.False(result);
        }

        [Fact]
        public void CreateUser_ValidUsername_CallsCreateOrUpdate()
        {
            // arrange
            var userRepositoryMock = new UserRepositoryFake();
            var sut = new UserService(userRepositoryMock);

            // act
            var result = sut.CreateUser("fake-user-name");

            // assert
            Assert.Equal(1, userRepositoryMock.CreateOrUpdateCalledTimes);
            Assert.Equal("fake-user-name", userRepositoryMock.CreateOrUpdateCalledWith.Name);
        }

        private class UserRepositoryFake : IRepository<User>
        {
            public User GetMethodResult { get; set; }
            public int CreateOrUpdateCalledTimes { get; set; }
            public User CreateOrUpdateCalledWith { get; set; }

            public List<User> All()
            {
                return new List<User> { };
            }

            public void CreateOrUpdate(User entity)
            {
                CreateOrUpdateCalledTimes++;
                CreateOrUpdateCalledWith = entity;
            }

            public List<User> Find(Func<User, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public User Get(Func<User, bool> predicate)
            {
                return GetMethodResult;
            }

            public User Get(int id)
            {
                throw new NotImplementedException();
            }
        }
    }
}
