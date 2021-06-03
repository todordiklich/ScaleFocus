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
            Owners = new List<User>();
        }

        public string Description { get; set; }

        public string Name { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<User> Owners { get; set; }
    }
}
