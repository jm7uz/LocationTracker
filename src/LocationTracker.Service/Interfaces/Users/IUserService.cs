using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Configurations;

namespace LocationTracker.Service.Interfaces.Users;

public interface IUserService
{
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<UserForResultDto> ModifyAttachAreaAsync(long id, int AttachAreaModifyId);
    Task<UserForResultDto> ModifyRoleAsync(long id, short roleId);

}
