using System;
using System.Linq;
using System.Collections.Generic;

using SimpleToDoApp.Data;
using SimpleToDoApp.Models;

namespace SimpleToDoApp.Services
{
    public class UserService
    {
        private const string StoreFileName = "Users.json";

        private readonly FileDatabase _storage;

        private readonly List<User> _applicationUsers = new List<User>();

        public User CurrentUser { get; private set; }

        public UserService()
        {
            _storage = new FileDatabase();
            List<User> usersFromFile = _storage.Read<List<User>>(StoreFileName);
            if (usersFromFile == null)
            {
                string userName = "admin";
                string password = "adminpassword";
                string firstName = "admin";
                string lastName = "admin";
                bool isAdmin = true;

                CreateAdmin(userName, password, firstName, lastName, isAdmin);
            }
            else
            {
                _applicationUsers = usersFromFile;
            }
        }

        public HashSet<int> GetAllUsersId()
        {
            return _applicationUsers.Select(u => u.Id).ToHashSet();
        }
        public bool DeleteUserById(int userId) 
        {
            User userToDelete = GetUserById(userId);

            if (userToDelete == null)
            {
                return false;
            }

            _applicationUsers.Remove(userToDelete);

            SaveToFile();

            return true;
        }
        public User GetUserById(int userId)
        {
            User user = _applicationUsers.FirstOrDefault(u => u.Id == userId);

            return user;
        }
        public IReadOnlyList<User> ListAllUsers()
        {
            return _applicationUsers.AsReadOnly();
        }
        private bool CreateAdmin(string userName, string password, string firstName, string lastName, bool isAdmin)
        {
            int newUniqueId = _applicationUsers.Count + 1;

            DateTime now = DateTime.Now;

            var user = new User(newUniqueId, now, newUniqueId, now, newUniqueId, userName, password, firstName, lastName, isAdmin);

            _applicationUsers.Add(user);

            SaveToFile();

            return true;
        }

        public bool CreateUser(string userName, string password, string firstName, string lastName, bool isAdmin, User creator)
        {
            if (_applicationUsers.Any(u => u.UserName == userName))
            {
                return false;
            }

            int newUniqueId = _applicationUsers.Count + 1;

            DateTime now = DateTime.Now;

            var user = new User(newUniqueId, now, creator.Id, now, creator.Id, userName, password, firstName, lastName, isAdmin);

            _applicationUsers.Add(user);

            SaveToFile();

            return true;
        }

        private void SaveToFile()
        {
            _storage.Write(StoreFileName, _applicationUsers);
        }
        public void SaveChanges()
        {
            SaveToFile();
        }

        public void Login(string userName, string password)
        {
            CurrentUser = _applicationUsers.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }

        public void LogOut()
        {
            CurrentUser = null;
        }
    }
}
