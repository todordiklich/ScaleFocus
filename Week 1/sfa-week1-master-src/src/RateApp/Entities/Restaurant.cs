using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.Entities
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

        public User Owner { get; set; }
    }
}
