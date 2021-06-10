using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using RateApp.Model.Requests;
using RateApp.Model.Responses;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
            : base()
        {
            _userService = userService;
        }

        // POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post(UserRequestDTO user)
        {

            User currentUser = await _userService.GetCurrentUser(User);

            // This is not needed
            if (await _userService.IsUserInRole(currentUser.Id, "Admin"))
            {
                bool result = await _userService.CreateUser(user.UserName, user.Password);

                if (result)
                {
                    User userFromDB = await _userService.GetUserByName(user.UserName);

                    return CreatedAtAction("Get", "Users", new { id = userFromDB.Id }, null);
                }
                else
                {
                    return BadRequest();
                }
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserResponseDTO> Get(string id)
        {
            User userFromDB = await _userService.GetUserById(id);
            return new UserResponseDTO()
            {
                UserName = userFromDB.UserName,
                Id = userFromDB.Id,
            };
        }

        [HttpGet]
        [Route("All")]
        public async Task<List<UserResponseDTO>> GetAll()
        {
            List<UserResponseDTO> users = new List<UserResponseDTO>();

            foreach (var user in await _userService.GetAll())
            {
                users.Add(new UserResponseDTO()
                {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    UserName = user.UserName
                });
            }

            return users;
        }
    }
}
