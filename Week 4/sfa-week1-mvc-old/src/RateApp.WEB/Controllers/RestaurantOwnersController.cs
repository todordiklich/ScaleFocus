using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateApp.DAL.Data;
using RateApp.Model.Requests;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantOwnersController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;
        public RestaurantOwnersController()
        {
            _restaurantService = new RestaurantService(new DatabaseContext());
        }

        [HttpPost]
        public IActionResult Post(RestaurantOwnerRequestDTO restaurantOwner)
        {
            if (ModelState.IsValid)
            {
                _restaurantService.AddOwner(restaurantOwner.RestaurantId, restaurantOwner.UserId);
                return Ok();
            }
            return BadRequest();
        }
    }
}
