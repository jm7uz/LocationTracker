namespace LocationTracker.Service.DTOs.Locations.LocationReports;

public class LocationReportForCreationDto
{
    public long UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public short StatusId { get; set; }
}
