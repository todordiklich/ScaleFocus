using RateApp.DAL.Abstraction;
using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.Services
{
    /// <summary>
    /// Manages restaurant related functionality
    /// </summary>
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Review> _reviewRepsitory;
        private readonly IRepository<Restaurant> _restaurantRepository;
        private readonly IRepository<User> _userRepository;

        public RestaurantService(IRepository<Restaurant> restaurantRepository,
                                 IRepository<Review> reviewRepository,
                                 IRepository<User> userRepository)
        {
            _reviewRepsitory = reviewRepository;
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        public List<Restaurant> GetAll()
        {
            return _restaurantRepository.All();
        }

        public int CreateRestaurant(string name, string description, int ownerId)
        {
            User owner = _userRepository.Get(ownerId);
            Restaurant newRestaurant = new()
            {
                Name = name,
                Description = description,
                Owners = new List<User>() { owner }
            };

            _restaurantRepository.CreateOrUpdate(newRestaurant);


            return newRestaurant.Id;
        }

        public Restaurant GetRestaurant(int id)
        {
            return _restaurantRepository.Get(id);
        }

        public bool AddReview(int restaurantId, int reviewerId, string review, int rating)
        {
            if (DateTimeProvider.Current.UtcNow.DayOfWeek == DayOfWeek.Monday)
            {
                rating += 1;
            }

            User reviewer = _userRepository.Get(reviewerId);
            Restaurant restaurant = _restaurantRepository.Get(restaurantId);
            Review newReview = new()
            {
                Rating = rating,
                Reviewer = reviewer,
                ReviewText = review,
                Restaurant = restaurant
            };

            _reviewRepsitory.CreateOrUpdate(newReview);

            return newReview.Id != 0;
        }

        public void AddOwner(int restaurantId, int ownerId)
        {
            Restaurant restaurant = _restaurantRepository.Get(restaurantId);
            User owner = _userRepository.Get(ownerId);
            if (!restaurant.Owners.Any(o => o.Id == owner.Id))
            {
                restaurant.Owners.Add(owner);
            }
        }

        public List<Review> GetReviewsByUser(int reviewerId)
        {
            List<Review> reviews = _reviewRepsitory.Find(r => r.Reviewer.Id == reviewerId);
            return reviews;
        }

        public List<Review> GetReviewsForRestaurant(int restaurantId)
        {
            List<Review> reviews = _reviewRepsitory.Find(r => r.Restaurant.Id == restaurantId);
            return reviews;
        }
    }
}
