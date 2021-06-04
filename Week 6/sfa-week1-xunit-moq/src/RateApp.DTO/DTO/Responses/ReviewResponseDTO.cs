using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Responses
{
    public class ReviewResponseDTO : BaseResponseDTO
    {
        public string ReviewerName { get; set; }

        public string RestaurantName { get; set; }

        public string Review { get; set; }

        public int Rating { get; set; }
    }
}
