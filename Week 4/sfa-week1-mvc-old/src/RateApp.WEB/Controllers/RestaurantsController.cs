using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using RateApp.Model.Requests;
using RateApp.Model.Responses;
using RateApp.Services;
using RateApp.WEB.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;
        private readonly UserService _userService;

        public RestaurantsController()
        {
            var database = new DatabaseContext();
            _restaurantService = new RestaurantService(database);
            _userService = new UserService(database);
        }

        [HttpGet]
        [Route("{id}")]
        public RestaurantResponseDTO Get(int id)
        {
            Restaurant restaurantFromDb = _restaurantService.GetRestaurant(id);
            return MapRestaurant(restaurantFromDb);
        }

        [HttpPost]
        public IActionResult Post(RestaurantRequestDTO restaurant)
        {
            if (ModelState.IsValid)
            {
                User currentUser = _userService.GetCurrentUser(Request);
                int newRestaurantId = _restaurantService.CreateRestaurant(restaurant.Name, restaurant.Description, currentUser.Id);

                return CreatedAtAction("Get", "Restaurants", new { id = newRestaurantId }, restaurant);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("All")]
        public List<RestaurantResponseDTO> GetAll()
        {
            List<RestaurantResponseDTO> restaurantsResponse = new List<RestaurantResponseDTO>();

            foreach (var restaurant in _restaurantService.GetAll())
            {
                RestaurantResponseDTO restaurantDto = MapRestaurant(restaurant);
                restaurantsResponse.Add(restaurantDto);
            }
            return restaurantsResponse;
        }

        private RestaurantResponseDTO MapRestaurant(Restaurant restaurant)
        {
            var restaurantDto = new RestaurantResponseDTO()
            {
                Id = restaurant.Id,
                CreatedAt = restaurant.CreatedAt,
                Name = restaurant.Name,
                Description = restaurant.Description
            };

            foreach (var review in restaurant.Reviews)
            {
                restaurantDto.Reviews.Add(new ReviewResponseDTO()
                {
                    CreatedAt = review.CreatedAt,
                    Id = review.Id,
                    Rating = review.Rating,
                    RestaurantName = restaurant.Name,
                    Review = review.ReviewText,
                    ReviewerName = review.Reviewer.Name
                });
            }

            foreach (var owner in restaurant.Owners)
            {
                restaurantDto.Onwers.Add(new UserResponseDTO()
                {
                    CreatedAt = owner.CreatedAt,
                    Id = owner.Id,
                    UserName = owner.Name
                });
            }
            return restaurantDto;
        }
    }
}
