using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.UserLocations;

namespace LocationTracker.Service.Interfaces.Locations;

public interface IUserLocationService
{
    Task<bool> RemoveAsync(long id);
    Task<UserLocationForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<UserLocationForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<UserLocationForResultDto> AddAsync(UserLocationForCreationDto dto);
    Task<UserLocationForResultDto> ModifyAsync(long id, UserLocationForUpdateDto dto);
}
