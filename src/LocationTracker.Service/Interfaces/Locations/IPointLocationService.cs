using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.PointLocations;

namespace LocationTracker.Service.Interfaces.Locations;

public interface IPointLocationService
{
    Task<bool> RemoveAsync(int id);
    Task<PointLocationForResultDto> RetrieveByIdAsync(int id);
    Task<IEnumerable<PointLocationForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<PointLocationForResultDto> AddAsync(PointLocationForCreationDto dto);
    Task<PointLocationForResultDto> ModifyAsync(int id, PointLocationForUpdateDto dto);
}
