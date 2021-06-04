using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Responses
{
    public class RestaurantResponseDTO : BaseResponseDTO
    {
        public RestaurantResponseDTO()
        {
            Reviews = new List<ReviewResponseDTO>();
            Onwers = new List<UserResponseDTO>();
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ReviewResponseDTO> Reviews { get; set; }
        public List<UserResponseDTO> Onwers { get; set; }
    }
}
