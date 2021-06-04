using Microsoft.AspNetCore.Http;
using RateApp.DAL.Entities;

namespace RateApp.WEB.Auth
{
    public interface IAuthProvider
    {
        User GetCurrentUser(HttpRequest request);
    }
}
