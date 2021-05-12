using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.Services
{
    /// <summary>
    /// Manages restaurant related functionality
    /// </summary>
    public class RestaurantService
    {
        private readonly DatabaseContext _database;

        public RestaurantService(DatabaseContext database)
        {
            _database = database;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _database.Restaurants.Include(r => r.Reviews).ToList();
        }

        public bool CreateRestaurant(string name, string description, User owner)
        {
            Restaurant newRestaurant = new()
            {
                Name = name,
                Description = description,
                Owners = new List<User>() { owner }
            };

            _database.Restaurants.Add(newRestaurant);

            _database.SaveChanges();

            return newRestaurant.Id != 0;
        }

        public Restaurant GetRestaurant(int id)
        {
            return _database.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public bool AddReview(Restaurant restaurant, User reviewer, string review, int rating)
        {
            Review newReview = new()
            {
                Rating = rating,
                Reviewer = reviewer,
                ReviewText = review,
                Restaurant = restaurant
            };

            _database.Reviews.Add(newReview);

            _database.SaveChanges();

            return newReview.Id != 0;
        }

        public List<Review> GetReviewsByUser(int reviewerId)
        {
            List<Review> reviews = _database.Reviews
                .Where(r => r.Reviewer.Id == reviewerId)
                .Include(r => r.Restaurant)
                .Include(r => r.Reviewer).ToList();

            return reviews;
        }

        public List<Review> GetReviewsForRestaurant(int restaurantId)
        {
            List<Review> reviews = _database.Reviews
                .Where(r => r.Restaurant.Id == restaurantId)
                .Include(r => r.Restaurant)
                .Include(r => r.Reviewer)
                .ToList();

            return reviews;
        }
    }
}
