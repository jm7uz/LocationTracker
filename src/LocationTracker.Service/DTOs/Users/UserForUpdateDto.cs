namespace LocationTracker.Service.DTOs.Users;

public class UserForUpdateDto
{
    public string FullName { get; set; }
    public string ProfileImagePath { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int AttachedArea { get; set; }
}
