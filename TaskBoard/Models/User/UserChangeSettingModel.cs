using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TaskBoard.Models.Enums;

namespace TaskBoard.Models.User
{
    public class UserChangeSettingModel
    {
        public string PhotoLink { get; set; }
        public IFormFile Photo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Education { get; set; }
        public OrderTheme InterestedTheme { get; set; }
        public string Information { get; set; }

        [PasswordChangeCheck]
        public string OldPassword { get; set; }

        public string Password { get; set; }

        [PasswordChangeCheck]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать!")]
        public string CheckNewPassword { get; set; }

        public static explicit operator UserModel(UserChangeSettingModel userChange)
        {
            return new UserModel
            {
                Phone = userChange.Phone,
                Password = userChange.Password,
                Information = userChange.Information,
                InterestedTheme = userChange.InterestedTheme,
                Email = userChange.Email,
                Education = userChange.Education
            };
        }
    }
}
