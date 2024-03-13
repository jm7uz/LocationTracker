using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationTracker.Service.Interfaces.Regions;
 
public interface IRegionService
{
    Task<bool> RemoveAsync(int id);
    Task<RegionForResultDto> RetrieveByIdAsync(int id);
    Task<IEnumerable<RegionForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<RegionForResultDto> AddAsync(RegionForCreationDto dto);
    Task<RegionForResultDto> ModifyAsync(int id, RegionForUpdateDto dto);
}
