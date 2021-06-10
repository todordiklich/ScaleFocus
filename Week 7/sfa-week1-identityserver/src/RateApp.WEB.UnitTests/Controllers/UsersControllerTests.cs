using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RateApp.DAL.Entities;
using RateApp.Model.Requests;
using RateApp.Services;
using RateApp.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace RateApp.WEB.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public void Post_UnauthorizedUser_Returns401()
        {
            // arrange
            
            var userService = new Mock<IUserService>();
            userService.Setup(ap => ap.GetCurrentUser(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new User { UserName = "not-yavor" });
            
            var sut = new UsersController(userService.Object);
            var userRequestDTO = new UserRequestDTO { };

            // act
            var result = sut.Post(userRequestDTO);

            // assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
