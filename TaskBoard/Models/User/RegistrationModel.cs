using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TaskBoard.Models.Enums;

namespace TaskBoard.Models.User
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Введите email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите уникальное имя на сайте")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите телефон")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Вы не ввели пароль повторно")]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать!")]
        public string CheckPassword { get; set; }
        public string Education { get; set; }
        public OrderTheme InterestedTheme { get; set; }
        public string Information { get; set; }
        public IFormFile Photo { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Должно быть дано согласие на обработку персональных данных.")]
        public bool Agreement { get; set; }

        public static explicit operator UserModel(RegistrationModel registrationModel)
        {
            return new UserModel
            {
                Name = registrationModel.Name,
                SecondName = registrationModel.SecondName,
                Phone = registrationModel.Phone,
                Password = registrationModel.Password,
                Information = registrationModel.Information,
                InterestedTheme = registrationModel.InterestedTheme,
                Email = registrationModel.Email,
                Education = registrationModel.Education,
                Login = registrationModel.Login,
                WorksOrdered = 0,
                WorksPerformed = 0
            };
        }

    }
}
