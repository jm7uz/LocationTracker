using LocationTracker.Service.DTOs.Locations.LocationChecker;
using System.Text.Json.Serialization;

namespace LocationTracker.Service.DTOs.Locations.LocationChecker;

public class DeterminePositionRequest
{
    [JsonPropertyName("wantedPersonLocation")]
    public LocationCheckerProperty WantedPersonLocation { get; set; }

    [JsonPropertyName("borderPoints")]
    public List<LocationCheckerProperty> BorderPoints { get; set; }
}