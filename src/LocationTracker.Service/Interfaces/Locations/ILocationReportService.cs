using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using LocationTracker.Service.DTOs.Locations.LocationReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationTracker.Service.Interfaces.Locations;

public interface ILocationReportService
{
    Task<bool> RemoveAsync(long id);
    Task<LocationReportForResultDto> RetrieveByIdAsync(long id);
    Task<LocationReportForResultDto> CreateAsync(LocationReportForCreationDto dto);
    Task<LocationReportForResultDto> ModifyAsync(long id, LocationReportForUpdateDto dto);
    Task<IEnumerable<LocationReportForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
