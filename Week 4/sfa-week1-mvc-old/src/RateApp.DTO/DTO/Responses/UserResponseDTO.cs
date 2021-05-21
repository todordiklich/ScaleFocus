using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Responses
{
    public class UserResponseDTO : BaseResponseDTO
    {
        public string UserName { get; set; }
    }
}
