using LocationTracker.Api.Controllers.Common;
using LocationTracker.Domain.Enums;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LocationTracker.Api.Controllers.Users
{
    [EnableRateLimiting("fixed")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
            [Authorize(Roles = "Admin, SuperAdmin")]
            [HttpPost]
            public async Task<IActionResult> InsertAsync([FromForm] UserForCreationDto dto)
                => Ok(await _userService.CreateAsync(dto));

            [Authorize(Roles = "Admin, SuperAdmin")]
            [HttpGet]
            public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
                => Ok(await _userService.RetrieveAllAsync(@params));

            [Authorize(Roles = "Admin, SuperAdmin")]
            [HttpGet("{id}")]
            public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
                => Ok(await _userService.RetrieveByIdAsync(id));

            [Authorize(Roles = "Admin, SuperAdmin")]
            [HttpDelete("{id}")]
            public async Task<IActionResult> RemoveAsync([FromRoute] long id)
                => Ok(await _userService.RemoveAsync(id));

            [Authorize(Roles = "Admin, SuperAdmin")]
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromForm] UserForUpdateDto dto)
                => Ok(await _userService.ModifyAsync(id, dto));

            [Authorize(Roles = "SuperAdmin")]
            [HttpPatch()]
            public async Task<IActionResult> ModifyStatus(long id, Role roleId)
                => Ok(await _userService.ModifyRoleAsync(id, roleId));
        }
}
