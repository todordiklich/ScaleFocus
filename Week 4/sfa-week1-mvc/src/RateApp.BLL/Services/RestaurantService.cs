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
    public class RestaurantService
    {
        private readonly DatabaseContext _database;

        public RestaurantService(DatabaseContext database)
        {
            _database = database;
        }

        public List<Restaurant> GetAll()
        {
            return _database.Restaurants.ToList();
        }

        public int CreateRestaurant(string name, string description, int ownerId)
        {
            User owner = _database.Users.FirstOrDefault(u => u.Id == ownerId);
            Restaurant newRestaurant = new()
            {
                Name = name,
                Description = description,
                Owners = new List<User>() { owner }
            };

            _database.Restaurants.Add(newRestaurant);

            _database.SaveChanges();

            return newRestaurant.Id;
        }

        public Restaurant GetRestaurant(int id)
        {
            return _database.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public bool AddReview(int restaurantId, int reviewerId, string review, int rating)
        {

            User reviewer = _database.Users.FirstOrDefault(r => r.Id == reviewerId);
            Restaurant restaurant = _database.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
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

        public void AddOwner(int restaurantId, int ownerId)
        {
            Restaurant restaurant = _database.Restaurants.First(r=>r.Id == restaurantId);
            User owner = _database.Users.First(u => u.Id == ownerId);
            if (!restaurant.Owners.Any(o => o.Id == owner.Id))
            {
                restaurant.Owners.Add(owner);
                _database.SaveChanges();
            }
        }

        public List<Review> GetReviewsByUser(int reviewerId)
        {
            List<Review> reviews = _database.Reviews
                .Where(r => r.Reviewer.Id == reviewerId)
                .ToList();

            return reviews;
        }

        public List<Review> GetReviewsForRestaurant(int restaurantId)
        {
            List<Review> reviews = _database.Reviews
                .Where(r => r.Restaurant.Id == restaurantId)
                .ToList();

            return reviews;
        }
    }
}
