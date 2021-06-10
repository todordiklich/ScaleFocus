using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Requests
{
    public class ReviewRequestDTO
    {
        [Required]
        public string ReviewerId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Review { get; set; }

        [Range(1,10)]
        public int Rating { get; set; }
    }
}
