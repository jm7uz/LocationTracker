using LocationTracker.Domain.Commons;
using LocationTracker.Domain.Entities.Regions;

namespace LocationTracker.Domain.Entities.Districts;

public class District : Auditable<int>
{
    public string Name { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }
}
