using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Responses
{
    public class UserResponseDTO 
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
    }
}
