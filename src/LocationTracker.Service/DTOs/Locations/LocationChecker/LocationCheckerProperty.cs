using System.Text.Json.Serialization;

namespace LocationTracker.Service.DTOs.Locations.LocationChecker;

public class LocationCheckerProperty
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
