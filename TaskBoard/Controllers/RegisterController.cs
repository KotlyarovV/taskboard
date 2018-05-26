using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskBoard.Data;
using TaskBoard.Infrastucture;
using TaskBoard.Models;
using TaskBoard.Models.User;

namespace TaskBoard.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userStorage;
        private readonly IFinanceRepository _financeRepository;
        private readonly IFileRepository _fileRepository;
        
        public RegisterController(
            IUserRepository userStorage, 
            IFinanceRepository financeRepository,
            IFileRepository fileRepository
            )
        {
            _financeRepository = financeRepository;
            _userStorage = userStorage;
            _fileRepository = fileRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationModel registration)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewBag.Error = string.Join("\n", errors);
                return View("Index");
            }

            if (_userStorage.ExistenceVerification(registration.Login))
            {
                ViewBag.Error = "Пользователь с таким логином уже существует";
                return View("Index");
            }

            var user = (UserModel) registration;

            if (registration.Photo != null)
            {
                user.PhotoLink = await _fileRepository.SaveImage(registration.Photo);
            }

            _userStorage.Save(user);
            _financeRepository.AddAccount(user.Login);
            await HttpContext.LogIn(user.Login, "user");
            return Redirect("~/TaskBoard/Index");
        }
      
    }
}