using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SerializableDataBase;
using TaskBoard.Data;
using TaskBoard.Infrastucture;
using TaskBoard.Models.User;

namespace TaskBoard.Controllers
{
    /*
     * проверяет авторизацию
     * при наличии юзера в базе высылает куку
     * при авторизации перекидывает на /
     * иначе на /Home/Login
     *
     *logout - привыходе перекидвыает на  /Home/Login
     */
    public class AccountController : Controller
    {
        private readonly IUserRepository _userStorage;

        public AccountController(IUserRepository userStorage)
        {
            _userStorage = userStorage;
        }

        [HttpPost]
        public async Task<IActionResult> Authority(LoginModel login)
        {
            if (!_userStorage.CheckRegistration(login))
                return Redirect("/Home/Login");

            await HttpContext.LogIn(login.Username, "user");
            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Login");
        }
    }
}