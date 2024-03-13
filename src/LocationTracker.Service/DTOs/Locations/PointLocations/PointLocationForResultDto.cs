using LocationTracker.Service.DTOs.Locations.AttachedAreas;

namespace LocationTracker.Service.DTOs.Locations.PointLocations;

public class PointLocationForResultDto
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int AttachedAreaId { get; set; }
    public AttachedAreaForResultDto AttachedArea { get; set; }
}
