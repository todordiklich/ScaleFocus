using RateApp.DAL.Entities;
using System.Collections.Generic;

namespace RateApp.Services
{
    public interface IRestaurantService
    {
        void AddOwner(int restaurantId, int ownerId);
        bool AddReview(int restaurantId, int reviewerId, string review, int rating);
        int CreateRestaurant(string name, string description, int ownerId);
        List<Restaurant> GetAll();
        Restaurant GetRestaurant(int id);
        List<Review> GetReviewsByUser(int reviewerId);
        List<Review> GetReviewsForRestaurant(int restaurantId);
    }
}