using Microsoft.EntityFrameworkCore;
using RateApp.DAL.Abstraction;
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
    /// Responsible for managing user related functionality and tracking currently logged in user
    /// </summary>
    public class UserService : IUserService
    {

        private readonly IRepository<User> _repository;

        /// <summary>
        /// Initializes new instance of the UserService and creates a single default user 
        /// </summary>
        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="name">The name of the new User</param>
        /// <returns>True if user created otherwise false </returns>
        public bool CreateUser(string userName)
        {
            if (_repository.Get(u => u.Name == userName) != null)
            {
                return false;
            }

            User user = new User() { Name = userName };

            _repository.CreateOrUpdate(user);

            return true;
        }

        public User GetUserByName(string name)
        {
            return _repository.Get(u => u.Name == name);
        }

        public User GetUserById(int id)
        {
            return _repository.Get(id);
        }

        public List<User> GetAll()
        {
            return _repository.All();
        }
    }
}
