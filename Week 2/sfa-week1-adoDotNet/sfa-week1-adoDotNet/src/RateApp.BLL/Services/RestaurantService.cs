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
        private readonly Database _database;

        public RestaurantService(Database database)
        {
            _database = database;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = _database.GetRestaurants();
            foreach (Restaurant restaurant in restaurants)
            {
                restaurant.Owners = _database.GetRestaurantOwners(restaurant.Id);
            }
            return restaurants;
        }

        public bool CreateRestaurant(string name, string description, User owner)
        {
            Restaurant newRestaurant = new()
            {
                Name = name,
                Description = description
            };

            int newRestaurantId = _database.CreateRestaurant(newRestaurant);

            if (newRestaurantId > 0)
            {
               return  _database.AddRestaurantOwner(newRestaurantId, owner.Id);    
            }
            return false;
        }

        public Restaurant GetRestaurant(int id)
        {
            return _database.GetRestaurantById(id);
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

            return _database.AddReview(newReview);
        }

        public List<Review> GetReviewsByUser(int reviewerId)
        {
            List<Review> reviews = _database.GetReviewsByReviewerId(reviewerId);
            
            foreach (Review review in reviews)
            {
                review.Restaurant = _database.GetRestaurantById(review.RestaurantId);
                review.Reviewer = _database.GetUserById(review.ReviewerId);
            }

            return reviews;
        }

        public List<Review> GetReviewsForRestaurant(int restaurantId)
        {
            List<Review> reviews = _database.GetReviewsByRestaurantId(restaurantId);

            foreach (Review review in reviews)
            {
                review.Restaurant = _database.GetRestaurantById(review.RestaurantId);
                review.Reviewer = _database.GetUserById(review.ReviewerId);
            }

            return reviews;
        }

        public bool AddOwner(int restaurantId, int ownerId)
        {
            return _database.AddRestaurantOwner(restaurantId, ownerId);
        }
    }
}
