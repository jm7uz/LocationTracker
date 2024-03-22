namespace LocationTracker.Service.DTOs.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string ProfileImagePath { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int AttachedAreaId { get; set; }
    public short RoleId { get; set; }
}
