﻿using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Districts;
using LocationTracker.Service.Interfaces.Districts;
using Microsoft.AspNetCore.Mvc;

namespace LocationTracker.Api.Controllers.Districts;

public class DistrictsController : BaseController
{
    private readonly IDistrictService _service;
    public DistrictsController(IDistrictService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] DistrictForCreationDto dto) =>
        Ok(await _service.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params) =>
        Ok(await _service.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _service.RetrieveByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _service.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] int id, [FromBody] DistrictForUpdateDto dto) =>
        Ok(await _service.ModifyAsync(id, dto));
}
