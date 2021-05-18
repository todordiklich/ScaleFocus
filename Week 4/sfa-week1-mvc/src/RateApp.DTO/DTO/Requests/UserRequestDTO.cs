using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Requests
{
    public class UserRequestDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string UserName { get; set; }
    }
}
