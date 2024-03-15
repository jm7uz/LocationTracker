namespace LocationTracker.Service.DTOs.Locations.UserLocations;

public class UserLocationForCreationDto
{
    public long UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
