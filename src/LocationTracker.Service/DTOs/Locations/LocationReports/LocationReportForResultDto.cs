using LocationTracker.Domain.Enums;

namespace LocationTracker.Service.DTOs.Locations.LocationReports;

public class LocationReportForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public short StatusId { get; set; }
    public string Status { get; set; }
}
