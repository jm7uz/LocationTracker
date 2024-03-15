using LocationTracker.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LocationTracker.Service.DTOs.Logins;

public class LoginDto
{
    [Required(ErrorMessage = "Telefon raqamni kiriting"), PhoneNumber]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Parolni kiriting"), StrongPassword]
    public string Password { get; set; }
}
