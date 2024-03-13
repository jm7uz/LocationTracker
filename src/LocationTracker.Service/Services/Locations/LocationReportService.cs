using AutoMapper;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.Commons.Extentions;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.LocationReports;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Interfaces.Locations;
using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Service.Services.Locations;

public class LocationReportService : ILocationReportService
{
    private readonly IMapper _mapper;

    private readonly ILocationReportRepository _locationReportRepository;
    public LocationReportService(IMapper mapper, ILocationReportRepository locationReportRepository)
    {
        _mapper = mapper;
        _locationReportRepository = locationReportRepository;
    }

    public async Task<LocationReportForResultDto> CreateAsync(LocationReportForCreationDto dto)
    {
        var IsValidUserId = await _locationReportRepository.SelectAll()
            .Where(u => u.UserId == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (IsValidUserId is not null)
            throw new LocationTrackerException(409, "User is already exist.");

        var mapped = _mapper.Map<locationReport>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await _locationReportRepository.InsertAsync(mapped);

        return _mapper.Map<LocationReportForResultDto>(mapped);
    
    }

    public async Task<LocationReportForResultDto> ModifyAsync(long id, LocationReportForUpdateDto dto)
    {
        var report = await _locationReportRepository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (report is null)
            throw new LocationTrackerException(404, "Location Report is not found");

        var IsValidUserId = await _locationReportRepository.SelectAll()
            .Where(u => u.UserId == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (IsValidUserId is null)
            throw new LocationTrackerException(404, "User is not found");

        var mapped = _mapper.Map(dto, report);
        mapped.UpdatedAt = DateTime.UtcNow;
        await _locationReportRepository.UpdateAsync(mapped);

        return _mapper.Map<LocationReportForResultDto>(mapped);

    }

    public async Task<bool> RemoveAsync(long id)
    {
        var removereport = await _locationReportRepository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (removereport is null)
            throw new LocationTrackerException(404, "Location Report is not found");

        await _locationReportRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<LocationReportForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var reports = await _locationReportRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList<locationReport, long>(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<LocationReportForResultDto>>(reports);
    }

    public async Task<LocationReportForResultDto> RetrieveByIdAsync(long id)
    {
        var report = await _locationReportRepository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (report is null)
            throw new LocationTrackerException(404, "Location Report is not found");

        return _mapper.Map<LocationReportForResultDto>(report);
    }
}
