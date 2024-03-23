using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.PointLocations;
using LocationTracker.Service.DTOs.Locations.UserLocations;
using LocationTracker.Service.Interfaces.Locations;
using Microsoft.AspNetCore.Mvc;

namespace LocationTracker.Api.Controllers.Locations;

public class PointLocationsController : BaseController
{
    private readonly IPointLocationService _pointLocationService;

    public PointLocationsController(IPointLocationService pointLocationService)
    {
        _pointLocationService = pointLocationService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] PointLocationForCreationDto dto) =>
        Ok(await _pointLocationService.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params) =>
        Ok(await _pointLocationService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _pointLocationService.RetrieveByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _pointLocationService.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] int id, [FromBody] PointLocationForUpdateDto dto) =>
        Ok(await _pointLocationService.ModifyAsync(id, dto));
}
