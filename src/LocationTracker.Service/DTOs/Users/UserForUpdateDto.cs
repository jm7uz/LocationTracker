﻿using System.ComponentModel.DataAnnotations;

namespace LocationTracker.Service.DTOs.Users;

public class UserForUpdateDto
{
    [Required(ErrorMessage = "To'liq ism kiritish majburiy")]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? ProfileImagePath { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public int? AttachedArea { get; set; }
}
