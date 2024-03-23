using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Configurations;
using LocationTracker.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace LocationTracker.Service.Interfaces.Users;

public interface IUserService
{
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<UserForResultDto> ModifyAttachAreaAsync(long id, int AttachAreaModifyId);
    Task<bool> ModifyRoleAsync(long id, Role role);
    Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto password);
    Task<bool> UploadPhotoAsync(long id, IFormFile photoPath);

}
