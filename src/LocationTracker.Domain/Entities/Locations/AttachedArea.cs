using LocationTracker.Domain.Commons;
using LocationTracker.Domain.Entities.Districts;
using LocationTracker.Domain.Entities.Regions;

namespace LocationTracker.Domain.Entities.Locations;

public class AttachedArea : Auditable<int>
{
    public string AreaName { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }

    public int DistrictId { get; set; }
    public District District { get; set; }
}
