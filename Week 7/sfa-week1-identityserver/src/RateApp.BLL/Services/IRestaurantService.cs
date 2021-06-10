using RateApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateApp.Services
{
    public interface IRestaurantService
    {
        Task AddOwner(int restaurantId, string ownerId);
        Task<bool> AddReview(int restaurantId, string reviewerId, string review, int rating);
        Task<int> CreateRestaurant(string name, string description, string ownerId);
        List<Restaurant> GetAll();
        Restaurant GetRestaurant(int id);
        List<Review> GetReviewsByUser(string reviewerId);
        List<Review> GetReviewsForRestaurant(int restaurantId);
    }
}