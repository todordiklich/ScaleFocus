using RateApp.Data;
using RateApp.Entities;
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
        private const string StoreFileName = "Users.json";

        private readonly FileDatabase _storage;

        /// <summary>
        /// List of all application users
        /// </summary>
        private readonly List<User> _applicationUsers = new List<User>();

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User CurrentUser { get; private set; }

        /// <summary>
        /// Initializes new instance of the UserService and creates a single default user 
        /// </summary>
        public UserService()
        {
            _storage = new FileDatabase();
            List<User> usersFromFile = _storage.Read<List<User>>(StoreFileName);
            if (usersFromFile == null)
            {
                CreateUser("yavor");
            }
            else
            {
                _applicationUsers = usersFromFile;
            }
        }

        private void SaveToFile()
        {
            _storage.Write(StoreFileName, _applicationUsers);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="name">The name of the new User</param>
        /// <returns>True if user created otherwise false </returns>
        public bool CreateUser(string name)
        {
            if (_applicationUsers.Any(u => u.Name == name))
            {
                return false;
            }

            int newUniqueId = _applicationUsers.Count + 1;

            DateTime now = DateTime.Now;

            _applicationUsers.Add(new User() { Name = name, Id = newUniqueId, CreatedAt = now });

            SaveToFile();

            return true;
        }


        /// <summary>
        /// Logs the user in the system by storing the data in the CurrentUser variable
        /// </summary>
        /// <param name="userName">The name of the user to be logged in</param>
        public void Login(string userName)
        {
            CurrentUser = _applicationUsers.FirstOrDefault(u => u.Name == userName);
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
