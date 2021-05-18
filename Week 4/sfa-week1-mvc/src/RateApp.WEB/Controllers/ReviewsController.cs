using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateApp.DAL.Data;
using RateApp.Services;
using RateApp.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateApp.DAL.Entities;
using RateApp.Model.Responses;

namespace RateApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public ReviewsController()
        {
            _restaurantService = new RestaurantService(new DatabaseContext());
        }

        // POST api/Reviews
        [HttpPost]
        public IActionResult Post(ReviewRequestDTO review)
        {
            if (ModelState.IsValid)
            {
                _restaurantService.AddReview(review.RestaurantId, review.ReviewerId, review.Review, review.Rating);
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("[Action]")]
        public List<ReviewResponseDTO> Search([FromQuery] int userId)
        {
            List<Review> reviews = _restaurantService.GetReviewsByUser(userId);
            List<ReviewResponseDTO> response = new List<ReviewResponseDTO>();
            foreach (var review in reviews)
            {
                response.Add(new ReviewResponseDTO()
                {
                    Id = review.Id,
                    CreatedAt = review.CreatedAt,
                    Rating = review.Rating,
                    RestaurantName = review.Restaurant.Name,
                    Review = review.ReviewText,
                    ReviewerName = review.Reviewer.Name
                });

            }
            return response;
        }
    }
}
