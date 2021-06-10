using Microsoft.AspNetCore.Mvc;
using Moq;
using RateApp.Model.Requests;
using RateApp.Services;
using RateApp.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RateApp.WEB.UnitTests.Controllers
{
    public class RestaurantOwnersControllerTests
    {
        [Fact]
        public void Post_ValidModel_ShouldCallRestaurantService()
        {
            // arrange
            var restaurantServiceMock = new Mock<IRestaurantService>();
            var sut = new RestaurantOwnersController(restaurantServiceMock.Object);
            var restaurantOwner = new RestaurantOwnerRequestDTO { RestaurantId = 1, UserId = "as" };

            // act
            var result = sut.Post(restaurantOwner);

            // assert
            restaurantServiceMock.Verify(service => service.AddOwner(1, "as"), Times.Once);
        }

        [Fact]
        public void Post_InvalidModelState_ShouldReturnBadRequest()
        {
            // arrange
            var restaurantServiceStub = new Mock<IRestaurantService>();
            var sut = new RestaurantOwnersController(restaurantServiceStub.Object);
            sut.ModelState.AddModelError("error", "error error");

            // act
            var result = sut.Post(It.IsAny<RestaurantOwnerRequestDTO>());

            // assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
