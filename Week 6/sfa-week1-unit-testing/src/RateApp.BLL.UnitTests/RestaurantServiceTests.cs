using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace RateApp.BLL.UnitTests
{
    public class RestaurantServiceTests
    {
        [Fact]
        public void AddReview_ItIsMonday_RatingShouldBePolished()
        {
            // arrange
            DateTime MONDAY = new DateTime(2021, 5, 31);
            DateTimeProvider.Current = new TimeProviderFake { Now = MONDAY };
            var restaurantRepositoryStub = new RestaurantRepositoryFake();
            var reviewRepositoryMock = new ReviewRepositoryFake();
            var userRepositoryFake = new UserRepositoryFake();
            var reviewParams = new { RestId = 1, ReviewerId = 1, Review = "", Rating = 1 };
            var sut = new RestaurantService(restaurantRepositoryStub, reviewRepositoryMock, userRepositoryFake);

            // act
            sut.AddReview(reviewParams.RestId, reviewParams.ReviewerId, reviewParams.Review, reviewParams.Rating);

            // assert
            Assert.Equal(2, reviewRepositoryMock.CreatedOrUpdatedReview.Rating);
        }

        private class RestaurantRepositoryFake : IRepository<Restaurant>
        {
            public void CreateOrUpdate(Restaurant entity) { }

            public List<Restaurant> Find(Func<Restaurant, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public Restaurant Get(Func<Restaurant, bool> predicate)
            {
                throw new NotImplementedException();
            }

            List<Restaurant> IRepository<Restaurant>.All()
            {
                throw new NotImplementedException();
            }

            Restaurant IRepository<Restaurant>.Get(int id)
            {
                return new Restaurant { };
            }
        }

        private class ReviewRepositoryFake : IRepository<Review>
        {
            public Review CreatedOrUpdatedReview { get; set; }
            public List<Review> All()
            {
                throw new NotImplementedException();
            }

            public void CreateOrUpdate(Review entity)
            {
                CreatedOrUpdatedReview = entity;
            }

            public List<Review> Find(Func<Review, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public Review Get(Func<Review, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public Review Get(int id)
            {
                throw new NotImplementedException();
            }
        }

        private class UserRepositoryFake : IRepository<User>
        {
            public List<User> All()
            {
                throw new NotImplementedException();
            }

            public void CreateOrUpdate(User entity)
            {
                throw new NotImplementedException();
            }

            public List<User> Find(Func<User, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public User Get(Func<User, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public User Get(int id)
            {
                return new User { };
            }
        }
    }

    public class TimeProviderFake : IDateTimeProvider
    {
        public DateTime Now { get; set; }
        public DateTime UtcNow => Now;
    }
}
