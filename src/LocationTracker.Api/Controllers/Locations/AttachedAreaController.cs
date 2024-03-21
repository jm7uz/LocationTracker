using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.Interfaces.Locations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace LocationTracker.Api.Controllers.Locations;


[Authorize(Roles = "Admin, SuperAdmin")]
public class AttachedAreaController : BaseController
{
    private readonly IAttachedAreaService _service;
    public AttachedAreaController(IAttachedAreaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAttachedAreaAsync([FromBody] AttachedAreaForCreationDto dto) =>
    Ok(await _service.CreateAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAttachedAreaAsync([FromQuery] PaginationParams @params) =>
        Ok(await _service.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _service.RetrieveByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttachedAreaAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _service.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttachedAreaAsync([FromRoute(Name = "id")] int id, [FromBody] AttachedAreaForUpdateDto dto) =>
        Ok(await _service.ModifyAsync(id, dto));
}
