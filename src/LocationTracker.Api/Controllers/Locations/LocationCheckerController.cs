using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.DTOs.Locations.LocationChecker;
using LocationTracker.Service.Interfaces.Locations;
using Microsoft.AspNetCore.Mvc;

namespace LocationTracker.Api.Controllers.Locations
{
    public class LocationCheckerController : BaseController
    {
        private readonly ILocationCheckerService _locationCheckerService;

        public LocationCheckerController(ILocationCheckerService locationCheckerService)
        {
            _locationCheckerService = locationCheckerService;
        }

        [HttpPost("DeterminePosition")]
        public async Task<IActionResult> DeterminePosition([FromBody] DeterminePositionRequest model)
        {
            if (model == null || model.WantedPersonLocation == null || model.BorderPoints == null)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                Tuple<double, double> wantedPersonLocation = new Tuple<double, double>(model.WantedPersonLocation.Latitude, model.WantedPersonLocation.Longitude);
                List<Tuple<double, double>> borderPoints = new List<Tuple<double, double>>();
                foreach (var location in model.BorderPoints)
                {
                    borderPoints.Add(new Tuple<double, double>(location.Latitude, location.Longitude));
                }

                (bool inside, double distance) = await _locationCheckerService.DeterminePositionAsync(wantedPersonLocation, borderPoints);
                return Ok(new { inside, distance });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
