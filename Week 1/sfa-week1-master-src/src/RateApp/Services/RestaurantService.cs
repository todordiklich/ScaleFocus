using RateApp.Entities;
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
        private readonly List<Restaurant> _restaurants = new List<Restaurant>();

        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurants;
        }

        public bool CreateRestaurant(string name, string description, User owner)
        {
            if (_restaurants.Any(r => r.Name == name))
            {
                return false;
            }
            else
            {
                int newUniqueId = _restaurants.Count + 1;
                Restaurant newRestaurant = new Restaurant()
                {
                    Id = newUniqueId,
                    Name = name,
                    Description = description,
                    Owner = owner
                };

                _restaurants.Add(newRestaurant);

                return true;
            }
        }

        public Restaurant GetRestaurant(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public void Review(Restaurant restaurant, User reviewer, string review, int rating)
        {
            Review newReview = new Review()
            {
                Rating = rating,
                Reviewer = reviewer,
                ReviewText = review,
                CreatedAt = DateTime.Now,
                Restaurant = restaurant
            };

            restaurant.Reviews.Add(newReview);
        }

        public List<Review> GetReviewsByUser(int reviewerId)
        {
            return _restaurants.SelectMany(r=>r.Reviews.Where(r=>r.Reviewer.Id == reviewerId)).ToList();   
        }

        public List<Review> GetReviewsForRestaurant(int restaurantId)
        {
            Restaurant restaurant = _restaurants.FirstOrDefault(r => r.Id == restaurantId);
            return restaurant.Reviews;
        }
    }
}
