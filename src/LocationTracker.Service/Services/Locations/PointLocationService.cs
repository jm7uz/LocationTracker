using AutoMapper;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.Commons.Extentions;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.PointLocations;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Interfaces.Locations;
using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Service.Services.Locations;

public class PointLocationService : IPointLocationService
{
    private readonly IMapper _mapper;
    private readonly IPointLocationRepository _pointLocationRepository;
    private readonly IAttachedAreaRepository _attachedAreaRepository;

    public PointLocationService(
        IMapper mapper, 
        IPointLocationRepository pointLocationRepository,
        IAttachedAreaRepository attachedAreaRepository)
    {
        _mapper = mapper;
        _pointLocationRepository = pointLocationRepository;
        _attachedAreaRepository = attachedAreaRepository;
    }

    public async Task<PointLocationForResultDto> AddAsync(PointLocationForCreationDto dto)
    {
        var area = await _attachedAreaRepository
            .SelectAll()
            .Where(a => a.Id == dto.AttachedAreaId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (area is null)
            throw new LocationTrackerException(404, "Area not found");

        var location = await _pointLocationRepository.SelectAll()
             .Where(ul => ul.Longitude == dto.Longitude &&
             ul.Latitude == dto.Latitude)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (location is not null)
            throw new LocationTrackerException(409, "Point Location is already exist");

        var mapped = _mapper.Map<PointLocation>(dto);
        await _pointLocationRepository.InsertAsync(mapped);

        return _mapper.Map<PointLocationForResultDto>(mapped);
    }

    public async Task<PointLocationForResultDto> ModifyAsync(int id, PointLocationForUpdateDto dto)
    {
        var pointLocation = await _pointLocationRepository
            .SelectAll()
            .Where(pl => pl.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (pointLocation is null)
            throw new LocationTrackerException(404, "Point location not found");

        var area = await _attachedAreaRepository
            .SelectAll()
            .Where(a => a.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (area is null)
            throw new LocationTrackerException(404, "Area not found");

        var location = await _pointLocationRepository.SelectAll()
             .Where(ul => ul.Longitude == dto.Longitude &&
             ul.Latitude == dto.Latitude)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (location is null)
            throw new LocationTrackerException(404, "Point Location is not found");

        var mapped = _mapper.Map(dto, pointLocation);
        mapped.UpdatedAt = DateTime.UtcNow;
        await _pointLocationRepository.UpdateAsync(mapped);

        return _mapper.Map<PointLocationForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var pointLocation = await _pointLocationRepository.SelectAll()
            .Where(ul => ul.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (pointLocation is null)
            throw new LocationTrackerException(404, "Point Location is not found!");

        await _pointLocationRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<PointLocationForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var pointLocations = await _pointLocationRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList<PointLocation, int>(@params)
            .ToListAsync();

        return _mapper.Map < IEnumerable<PointLocationForResultDto>>(pointLocations);
    }

    public async Task<PointLocationForResultDto> RetrieveByIdAsync(int id)
    {
        var pointLocation = await _pointLocationRepository.SelectAll()
            .Where(ul => ul.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (pointLocation is null)
            throw new LocationTrackerException(404, "Point Location is not found!");

        return _mapper.Map<PointLocationForResultDto>(pointLocation);
    }
}
