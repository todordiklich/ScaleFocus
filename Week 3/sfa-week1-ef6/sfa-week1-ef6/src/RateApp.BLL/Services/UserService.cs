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
        /// Currently logged in user
        /// </summary>
        public User CurrentUser { get; private set; }

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
        public bool CreateUser(string name)
        {
            if (_database.Users.FirstOrDefault(u => u.Name == name) != null)
            {
                return false;
            }
            User newUser = new User() { Name = name };
            _database.Users.Add(newUser);
            _database.SaveChanges();

            return newUser.Id != 0;
        }

        /// <summary>
        /// Logs the user in the system by storing the data in the CurrentUser variable
        /// </summary>
        /// <param name="userName">The name of the user to be logged in</param>
        public void Login(string userName)
        {
            CurrentUser = _database.Users.FirstOrDefault(u => u.Name == userName);
        }

        /// <summary>
        /// Logs the user out of the system by removing the value of the CurrentUser variable
        /// </summary>
        public void LogOut()
        {
            CurrentUser = null;
        }
    }
}
