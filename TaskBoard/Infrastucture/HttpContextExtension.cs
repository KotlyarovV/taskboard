using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace TaskBoard.Infrastucture
{
    public static class HttpContextExtension
    {
        public static async Task LogIn(this HttpContext httpContext, string login, string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Actor, login),
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, role)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(userIdentity);
            await httpContext.SignInAsync(principal);
        }
    }
}
