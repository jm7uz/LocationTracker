using LocationTracker.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using LocationTracker.Service.DTOs.Users;
using LocationTracker.Api.Controllers.Common;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace LocationTracker.Api.Controllers.Users
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

       // [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromForm] UserForCreationDto dto)
                    => Ok(await _userService.CreateAsync(dto));

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _userService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
            => Ok(await _userService.RetrieveByIdAsync(id));

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] long id)
            => Ok(await _userService.RemoveAsync(id));

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPut("{id}/modifyrole")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromForm] UserForUpdateDto dto)
            => Ok(await _userService.ModifyAsync(id, dto));

        [Authorize(Roles = "SuperAdmin")]
        [HttpPatch()]
        public async Task<IActionResult> ModifyStatus(long id, Role roleId)
            => Ok(await _userService.ModifyRoleAsync(id, roleId));

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPut("{id}/changepassword")]
        public async Task<IActionResult> ChangePassword([FromRoute] long id, [FromForm] UserForChangePasswordDto dto)
            => Ok(await _userService.ChangePasswordAsync(id, dto));

        [HttpPost("{id}/upload-picture")]
        public async Task<IActionResult> UploadPictureAsync(long id, IFormFile file)
            => Ok(await _userService.UploadPhotoAsync(id, file));

        
    }
}
