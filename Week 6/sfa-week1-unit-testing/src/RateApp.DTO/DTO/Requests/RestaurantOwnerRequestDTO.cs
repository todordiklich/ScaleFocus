using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Requests
{
    public class RestaurantOwnerRequestDTO
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
