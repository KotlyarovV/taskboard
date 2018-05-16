using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Models.User
{
    public class PasswordChangeCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (UserChangeSettingModel) validationContext.ObjectInstance;
            if (user.Password == null)
            {
                return ValidationResult.Success;
            }
            if (user.Password != null && value != null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}
