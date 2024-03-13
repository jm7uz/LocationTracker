namespace LocationTracker.Service.DTOs.Locations.UserLocations;

public class UserLocationForUpdateDto
{
    public long UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsVerified { get; set; }
}
