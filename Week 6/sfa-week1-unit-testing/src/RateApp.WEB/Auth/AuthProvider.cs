using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RateApp.DAL.Entities;
using RateApp.Services;
using System.Linq;

namespace RateApp.WEB.Auth
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IUserService _userService;

        public AuthProvider(IUserService userService)
        {
            _userService = userService;
        }

        public User GetCurrentUser(HttpRequest request)
        {
            StringValues authHeaders;
            request.Headers.TryGetValue("Auth", out authHeaders);
            if (authHeaders.Count != 0)
            {
                string userName = authHeaders.First();
                return _userService.GetUserByName(userName);
            }
            return null;
        }
    }
}
