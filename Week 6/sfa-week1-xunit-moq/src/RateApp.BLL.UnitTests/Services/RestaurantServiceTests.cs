using Moq;
using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace RateApp.BLL.UnitTests.Services
{
    public class RestaurantServiceTests
    {
        [Fact]
        public void AddReview_ItIsMonday_RatingShouldBePolished()
        {
            // arrange
            DateTime MONDAY = new DateTime(2021, 5, 31);
            var restaurantRepositoryStub = new Mock<IRepository<Restaurant>>();
            var reviewRepositoryMock = new Mock<IRepository<Review>>(); 
            var userRepositoryFake = new Mock<IRepository<User>>();
            var reviewParams = new { RestId = 1, ReviewerId = 1, Review = "", Rating = 1 };
            var datetimeProviderStub = new Mock<IDateTimeProvider>();
            datetimeProviderStub.Setup(provider => provider.UtcNow)
                .Returns(MONDAY);
            var sut = new RestaurantService(restaurantRepositoryStub.Object, reviewRepositoryMock.Object, userRepositoryFake.Object, datetimeProviderStub.Object);

            // act
            sut.AddReview(reviewParams.RestId, reviewParams.ReviewerId, reviewParams.Review, reviewParams.Rating);

            // assert
            reviewRepositoryMock.Verify(reviewRepository => 
                reviewRepository.CreateOrUpdate(It.Is<Review>(r => r.Rating == 2)), 
                Times.Once);
        }

        [Fact]
        public void AddReview_ValidReview_ShouldReturnTrue()
        {
            // arrange
            var restaurantRepositoryStub = new Mock<IRepository<Restaurant>>();
            var reviewRepositoryStub = new Mock<IRepository<Review>>();
            var userRepositoryFake = new Mock<IRepository<User>>();
            var reviewParams = new { RestId = 1, ReviewerId = 1, Review = "", Rating = 1 };
            var datetimeProviderStub = new Mock<IDateTimeProvider>();
            reviewRepositoryStub.Setup(reviewRepo => reviewRepo.CreateOrUpdate(It.IsAny<Review>()))
                .Callback<Review>(review => review.Id = 123);
            var sut = new RestaurantService(restaurantRepositoryStub.Object, reviewRepositoryStub.Object, userRepositoryFake.Object, datetimeProviderStub.Object);

            // act
            var result = sut.AddReview(reviewParams.RestId, reviewParams.ReviewerId, reviewParams.Review, reviewParams.Rating);

            // assert
            Assert.True(result);
        }
    }
}
