using LocationTracker.Service.DTOs.Districts;
using LocationTracker.Service.DTOs.Regions;

namespace LocationTracker.Service.DTOs.Locations.AttachedAreas;

public class AttachedAreaForResultDto
{
    public int Id { get; set; }
    public string AreaName { get; set; }
    public int RegionId { get; set; }
    public RegionForResultDto Region { get; set; }
    public int DistrictId { get; set; }
    public DistrictForResultDto District { get; set; }
}
