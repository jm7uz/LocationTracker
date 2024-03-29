﻿using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Regions;
using LocationTracker.Service.Interfaces.Regions;
using Microsoft.AspNetCore.Mvc;

namespace LocationTracker.Api.Controllers.Regions;

public class RegionsController : BaseController
{
    private readonly IRegionService _service;
    public RegionsController(IRegionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddChannelAsync([FromBody] RegionForCreationDto dto) =>
        Ok(await _service.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllChannelAsync([FromQuery] PaginationParams @params) =>
        Ok(await _service.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _service.RetrieveByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChannelAsync([FromRoute(Name = "id")] int id) =>
        Ok(await _service.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChannelAsync([FromRoute(Name = "id")] int id, [FromBody] RegionForUpdateDto dto) =>
        Ok(await _service.ModifyAsync(id, dto));
}
