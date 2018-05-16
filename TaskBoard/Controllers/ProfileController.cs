using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;
using TaskBoard.Models.User;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;

        public ProfileController(IUserRepository userRepository, IFileRepository fileRepository)
        {
            _userRepository = userRepository;
            _fileRepository = fileRepository;
        }

        public IActionResult Index()
        {
            UserModel user = _userRepository.GetUser(User.Identity.Name);  
            return View(user);
        }

        public IActionResult Setting()
        {
            UserModel user = _userRepository.GetUser(User.Identity.Name);
            return View(user);
        }

        public async Task<IActionResult> ChangeSetting(UserChangeSettingModel userChange)
        {
            var updatedUser = (UserModel) userChange;
            
            if (userChange.Photo != null)
            {
                var photoSaveTask = _fileRepository.SaveImage(userChange.Photo);
                var tasks = new List<Task>() { photoSaveTask };
                if (userChange.PhotoLink != null)
                {
                    tasks.Add(_fileRepository.DeleteFile(userChange.PhotoLink));
                }

                await Task.WhenAll(tasks);
                updatedUser.PhotoLink = photoSaveTask.Result;
            }

            _userRepository.Update(User.Identity.Name, updatedUser);
            return Redirect("/profile/index");
        }
    }
}