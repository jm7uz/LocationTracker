﻿using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.UserLocations;
using LocationTracker.Service.Interfaces.Locations;
using Microsoft.AspNetCore.Mvc;

namespace LocationTracker.Api.Controllers.Locations;

public class UserLocationsController : BaseController
{
    private readonly IUserLocationService _service;
    public UserLocationsController(IUserLocationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] UserLocationForCreationDto dto) =>
        Ok(await _service.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params) =>
        Ok(await _service.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id) =>
        Ok(await _service.RetrieveByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id) =>
        Ok(await _service.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromBody] UserLocationForUpdateDto dto) =>
        Ok(await _service.ModifyAsync(id, dto));
}