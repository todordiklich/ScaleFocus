using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RateApp.DAL.Entities;
using RateApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateApp.WEB.Auth
{
    public static class AuthHelper
    {
        public static User GetCurrentUser(this UserService userService, HttpRequest request)
        {
            StringValues authHeaders;
            request.Headers.TryGetValue("Auth", out authHeaders);
            if (authHeaders.Count != 0)
            {
                string userName = authHeaders.First();
                return userService.GetUserByName(userName);
            }
            return null;
        }
    }
}
