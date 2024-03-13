namespace LocationTracker.Service.DTOs.Locations.PointLocations;

public class PointLocationForCreationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int AttachedAreaId { get; set; }
}
