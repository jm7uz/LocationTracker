using LocationTracker.Domain.Commons;

namespace LocationTracker.Domain.Entities.Locations;

public class PointLocation : Auditable<int>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int AttachedAreaId { get; set; }
    public AttachedArea AttachedArea { get; set; }
}
