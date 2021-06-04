using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RateApp.DAL.Entities;
using RateApp.Model.Requests;
using RateApp.Services;
using RateApp.WEB.Auth;
using RateApp.WEB.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace RateApp.WEB.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public void Post_UnauthorizedUser_Returns401()
        {
            // arrange
            var authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(ap => ap.GetCurrentUser(It.IsAny<HttpRequest>()))
                .Returns(new User { Name = "not-yavor" });
            var userService = new Mock<IUserService>();
            var sut = new UsersController(userService.Object, authProvider.Object);
            var userRequestDTO = new UserRequestDTO { };

            // act
            var result = sut.Post(userRequestDTO);

            // assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
