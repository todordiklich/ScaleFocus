using Microsoft.AspNetCore.Authorization;
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
using System.Threading.Tasks;

namespace RateApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController() : base()
        {
            _userService = new UserService(new DatabaseContext());
        }

        // POST api/Users
        [HttpPost]
        public IActionResult Post(UserRequestDTO user)
        {
            User currentUser = _userService.GetCurrentUser(Request);

            if (currentUser.Name == "yavor")
            {
                bool isCreated = _userService.CreateUser(user.UserName);

                if (isCreated && ModelState.IsValid)
                {
                    User userFromDB = _userService.GetUserByName(user.UserName);

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
        public UserResponseDTO Get(int id)
        {
            User userFromDB = _userService.GetUserById(id);
            return new UserResponseDTO() 
            { 
                UserName = userFromDB.Name, 
                Id = userFromDB.Id, 
                CreatedAt = userFromDB.CreatedAt 
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
                    UserName = user.Name
                });
            }

            return users;
        }
    }
}
