using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace RateApp.DAL.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Restaurants = new List<Restaurant>();
        }

        public DateTime CreatedAt { get; set; }

        public virtual List<Restaurant> Restaurants { get; set; }
    }
}