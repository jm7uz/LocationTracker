using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Districts;

namespace LocationTracker.Service.Interfaces.Districts;

public interface IDistrictService
{
    Task<bool> RemoveAsync(int id);
    Task<DistrictForResultDto> RetrieveByIdAsync(int id);
    Task<IEnumerable<DistrictForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<DistrictForResultDto> AddAsync(DistrictForCreationDto dto);
    Task<DistrictForResultDto> ModifyAsync(int id, DistrictForUpdateDto dto);
}
