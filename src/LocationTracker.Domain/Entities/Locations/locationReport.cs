using LocationTracker.Domain.Commons;
using LocationTracker.Domain.Enums;

namespace LocationTracker.Domain.Entities.Locations;

public class locationReport : Auditable<long>
{
    public long UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public short StatusId { get; set; }
    public Status Status { get; set; }
}
