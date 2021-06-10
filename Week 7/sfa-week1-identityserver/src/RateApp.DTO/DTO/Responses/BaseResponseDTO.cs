using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.Model.Responses
{
    public class BaseResponseDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
