using System.Collections.Generic;

namespace RateApp.DAL.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public virtual List<Restaurant> Restaurants { get; set; }
    }
}