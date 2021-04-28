using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.DAL.Entities
{
    public class Restaurant : Entity
    {
        public Restaurant()
        {
            Reviews = new List<Review>();
        }

        public string Description { get; set; }

        public string Name { get; set; }

        public List<Review> Reviews { get; set; }

        public List<User> Owners { get; set; }
    }
}
