using Microsoft.EntityFrameworkCore;
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
    public class UserService
    {
        private readonly DatabaseContext _database;

        /// <summary>
        /// Initializes new instance of the UserService and creates a single default user 
        /// </summary>
        public UserService(DatabaseContext database)
        {
            _database = database;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="name">The name of the new User</param>
        /// <returns>True if user created otherwise false </returns>
        public bool CreateUser(string userName)
        {
            if (_database.Users.FirstOrDefault(u => u.Name == userName) != null)
            {
                return false;
            }

            User user = new User() { Name = userName };

            _database.Users.Add(user);
            _database.SaveChanges();

            return true;
        }

        public User GetUserByName(string name)
        {
            return _database.Users.FirstOrDefault(u => u.Name == name);
        }

        public User GetUserById(int id)
        {
            return _database.Users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _database.Users.ToListAsync();
        }
    }
}
