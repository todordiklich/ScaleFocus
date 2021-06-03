using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateApp.DAL.Entities;
using RateApp.Model.Requests;
using RateApp.Services;
using RateApp.WEB.Auth;
using RateApp.WEB.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace RateApp.WEB.UnitTests
{
    public class UsersControllerTests
    {
        [Fact]
        public void Post_UnauthorizedUser_Returns401()
        {
            // arrange
            var authProvider = new AuthProviderFake();
            var userService = new UserServiceFake();
            var sut = new UsersController(userService, authProvider);
            var userRequestDTO = new UserRequestDTO { };

            // act
            var result = sut.Post(userRequestDTO);

            // assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }

    public class UserServiceFake : IUserService
    {
        public bool CreateUser(string userName)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }
    }

    public class AuthProviderFake : IAuthProvider
    {
        public User GetCurrentUser(HttpRequest request)
        {
            return new User { Name = "not-yavor" };
        }
    }
}
