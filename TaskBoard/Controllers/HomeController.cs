using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using SerializableDataBase;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _userStorage;

        public HomeController(IUserRepository userStorage)
        {
            _userStorage = userStorage;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("/TaskBoard/Index");
        }
    }
}