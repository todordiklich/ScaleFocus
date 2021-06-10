using RateApp.BLL.Services;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserManager _userManager;

        public RestaurantService(IRepository<Restaurant> restaurantRepository,
                                 IRepository<Review> reviewRepository,
                                 IUserManager userManager,
                                 IDateTimeProvider dateTimeProvider
                                 )
        {
            _reviewRepsitory = reviewRepository;
            _restaurantRepository = restaurantRepository;
            _dateTimeProvider = dateTimeProvider;
            _userManager = userManager;
        }

        public List<Restaurant> GetAll()
        {
            return _restaurantRepository.All();
        }

        public async Task<int> CreateRestaurant(string name, string description, string ownerId)
        {
            User owner = await _userManager.FindByIdAsync(ownerId);
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

        public async Task<bool> AddReview(int restaurantId, string reviewerId, string review, int rating)
        {
            if (_dateTimeProvider.UtcNow.DayOfWeek == DayOfWeek.Monday)
            {
                rating += 1;
            }

            User reviewer = await _userManager.FindByIdAsync(reviewerId);
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

        public async Task AddOwner(int restaurantId, string ownerId)
        {
            Restaurant restaurant = _restaurantRepository.Get(restaurantId);
            User owner = await _userManager.FindByIdAsync(ownerId);
            if (!restaurant.Owners.Any(o => o.Id == owner.Id))
            {
                restaurant.Owners.Add(owner);
            }
        }

        public List<Review> GetReviewsByUser(string reviewerId)
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
