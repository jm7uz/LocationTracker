using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;

namespace LocationTracker.Service.Interfaces.Locations;

public interface IAttachedAreaService
{
    Task<bool> RemoveAsync(int id);
    Task<AttachedAreaForResultDto> RetrieveByIdAsync(int id);
    Task<AttachedAreaForResultDto> CreateAsync(AttachedAreaForCreationDto dto);
    Task<AttachedAreaForResultDto> ModifyAsync(int id, AttachedAreaForUpdateDto dto);
    Task<IEnumerable<AttachedAreaForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
