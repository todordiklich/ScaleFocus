using RateApp.DAL.Entities;
using System.Collections.Generic;

namespace RateApp.Services
{
    public interface IUserService
    {
        bool CreateUser(string userName);
        List<User> GetAll();
        User GetUserById(int id);
        User GetUserByName(string name);
    }
}