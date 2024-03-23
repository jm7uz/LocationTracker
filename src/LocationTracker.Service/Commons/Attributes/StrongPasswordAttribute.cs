using LocationTracker.Service.Commons.Validations;
using System.ComponentModel.DataAnnotations;

namespace LocationTracker.Service.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success; // Allow null or empty passwords
            else
            {
                string password = value.ToString()!;
                if (password.Length < 8)
                    return new ValidationResult("Parol 8 ta belgidan kam bo'lmasligi kerak");
                else if (password.Length > 30)
                    return new ValidationResult("Parol 30 ta belgidan ko'p bo'lmasligi kerak");

                var result = PasswordValidator.IsStrong(password);

                if (!result.IsValid)
                    return new ValidationResult(result.Message);
                else
                    return ValidationResult.Success;
            }
        }
    }
}
