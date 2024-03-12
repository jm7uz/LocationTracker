using LocationTracker.Domain.Commons;
using LocationTracker.Domain.Enums;

namespace LocationTracker.Domain.Entities.Users;

public class User : Auditable<long>
{
    public string FullName { get; set; }
    public string Password { get; set; }
    public string ProfileImagePath { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int AttachedArea {  get; set; }
    public short RoleId { get; set; }
    public Role Role { get; set; }
}
