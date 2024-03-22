using LocationTracker.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LocationTracker.Service.DTOs.Users;

public class UserForCreationDto
{
    public long Id { get; set; }

    [Required(ErrorMessage = "To'liq ism kiritish majburiy")]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }

    [StrongPassword]
    public string? Password { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public int? AttachedAreaId { get; set; }
}
