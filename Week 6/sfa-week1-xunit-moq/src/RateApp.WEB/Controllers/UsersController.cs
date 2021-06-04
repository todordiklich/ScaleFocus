using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserService _userService;
        private readonly IAuthProvider _authProvider;

        public UsersController(IUserService userService, IAuthProvider authProvider) 
            : base()
        {
            _userService = userService;
            _authProvider = authProvider;
        }

        // POST api/Users
        [HttpPost]
        public IActionResult Post(UserRequestDTO user)
        {

            User currentUser = _authProvider.GetCurrentUser(Request);

            if (currentUser.Name == "yavor")
            {
                bool result = _userService.CreateUser(user.UserName);

                if (result)
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
            };
        }
        [HttpGet]
        [Route("All")]
        public List<UserResponseDTO> GetAll()
        {
            List<UserResponseDTO> users = new List<UserResponseDTO>();

            foreach (var user in _userService.GetAll())
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
