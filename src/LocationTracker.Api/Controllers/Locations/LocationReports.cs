using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.LocationReports;
using LocationTracker.Service.Interfaces.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocationTracker.Api.Controllers.Locations;
[Authorize(Roles = "Admin, SuperAdmin")]
public class LocationReports : BaseController
{
    private readonly ILocationReportService _locationReportService;

    public LocationReports(ILocationReportService locationReportService)
    {
        _locationReportService = locationReportService;
    }

    [HttpPost]
    public async Task<IActionResult> AddChannelAsync([FromBody] LocationReportForCreationDto dto) =>
        Ok(await _locationReportService.CreateAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllChannelAsync([FromQuery] PaginationParams @params) =>
        Ok(await _locationReportService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id) =>
        Ok(await _locationReportService.RetrieveByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChannelAsync([FromRoute(Name = "id")] long id) =>
        Ok(await _locationReportService.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChannelAsync([FromRoute(Name = "id")] long id, [FromBody] LocationReportForUpdateDto dto) =>
        Ok(await _locationReportService.ModifyAsync(id, dto));
}
