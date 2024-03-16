using LocationTracker.Domain.Commons;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Domain.Enums;

namespace LocationTracker.Domain.Entities.Users;

public class User : Auditable<long>
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Password { get; set; }
    public string? ProfileImagePath { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Address { get; set; }
    public int? AttachedAreaId {  get; set; }
    public Role Role { get; set; }
    public string? Salt { get; set; }
}
