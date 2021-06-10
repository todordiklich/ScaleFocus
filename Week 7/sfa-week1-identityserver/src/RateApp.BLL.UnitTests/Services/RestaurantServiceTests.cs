using Moq;
using RateApp.BLL.Services;
using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RateApp.BLL.UnitTests.Services
{
    public class RestaurantServiceTests
    {
        [Fact]
        public async Task AddReview_ItIsMonday_RatingShouldBePolished()
        {
            // arrange
            DateTime MONDAY = new DateTime(2021, 5, 31);
            var restaurantRepositoryStub = new Mock<IRepository<Restaurant>>();
            var reviewRepositoryMock = new Mock<IRepository<Review>>(); 
            var userManagerfake = new Mock<IUserManager>();
            var reviewParams = new { RestId = 1, ReviewerId = "1", Review = "", Rating = 1 };
            var datetimeProviderStub = new Mock<IDateTimeProvider>();
            datetimeProviderStub.Setup(provider => provider.UtcNow)
                .Returns(MONDAY);
            var sut = new RestaurantService(restaurantRepositoryStub.Object, reviewRepositoryMock.Object, userManagerfake.Object, datetimeProviderStub.Object);

            // act
            await sut.AddReview(reviewParams.RestId, reviewParams.ReviewerId, reviewParams.Review, reviewParams.Rating);

            // assert
            reviewRepositoryMock.Verify(reviewRepository => 
                reviewRepository.CreateOrUpdate(It.Is<Review>(r => r.Rating == 2)), 
                Times.Once);
        }

        [Fact]
        public async Task AddReview_ValidReview_ShouldReturnTrue()
        {
            // arrange
            var restaurantRepositoryStub = new Mock<IRepository<Restaurant>>();
            var reviewRepositoryStub = new Mock<IRepository<Review>>();
            var userManagerfake = new Mock<IUserManager>();
            var reviewParams = new { RestId = 1, ReviewerId = "1", Review = "", Rating = 1 };
            var datetimeProviderStub = new Mock<IDateTimeProvider>();
            reviewRepositoryStub.Setup(reviewRepo => reviewRepo.CreateOrUpdate(It.IsAny<Review>()))
                .Callback<Review>(review => review.Id = 123);
            var sut = new RestaurantService(restaurantRepositoryStub.Object, reviewRepositoryStub.Object, userManagerfake.Object, datetimeProviderStub.Object);

            // act
            var result =  await sut.AddReview(reviewParams.RestId, reviewParams.ReviewerId, reviewParams.Review, reviewParams.Rating);

            // assert
            Assert.True(result);
        }
    }
}
