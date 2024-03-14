using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Configurations;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.Interfaces.Locations;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using LocationTracker.Service.Commons.Extentions;

namespace LocationTracker.Service.Services.Locations;

public class AttachedAreaService : IAttachedAreaService
{
    private readonly IMapper _mapper;
    private readonly IAttachedAreaRepository _attachedAreaRepository;

    public AttachedAreaService(IMapper mapper, IAttachedAreaRepository attachedAreaRepository)
    {
        _mapper = mapper;
        _attachedAreaRepository = attachedAreaRepository;
    }

    public async Task<AttachedAreaForResultDto> CreateAsync(AttachedAreaForCreationDto dto)
    {
        var attachedArea = await _attachedAreaRepository.SelectAll()
            .Where(a => a.AreaName.ToLower() == dto.AreaName.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (attachedArea is not null)
            throw new LocationTrackerException(409, "Attached Area is already exist.");

        var mapppedAttachedArea = _mapper.Map<AttachedArea>(dto);
        mapppedAttachedArea.CreatedAt = DateTime.UtcNow;

        var createdAttachedArea = await _attachedAreaRepository.InsertAsync(mapppedAttachedArea);

        return _mapper.Map<AttachedAreaForResultDto>(createdAttachedArea);
    }

    public async Task<AttachedAreaForResultDto> ModifyAsync(int id, AttachedAreaForUpdateDto dto)
    {
        var attachedArea = await _attachedAreaRepository.SelectByIdAsync(id);

        if (attachedArea is null)
            throw new LocationTrackerException(404, "Attached Area is not found");

        var mappedAttachedArea = _mapper.Map(dto, attachedArea);
        mappedAttachedArea.UpdatedAt = DateTime.UtcNow;

        await _attachedAreaRepository.UpdateAsync(mappedAttachedArea);

        return _mapper.Map<AttachedAreaForResultDto>(mappedAttachedArea);
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var attachedArea = await _attachedAreaRepository.SelectByIdAsync(id);

        if (attachedArea is null)
            throw new LocationTrackerException(404, "Attached Area is not found");

        return await _attachedAreaRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AttachedAreaForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var attachedAreas = await _attachedAreaRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList<AttachedArea, int>(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AttachedAreaForResultDto>>(attachedAreas);
    }

    public async Task<AttachedAreaForResultDto> RetrieveByIdAsync(int id)
    {
        var attachedArea = await _attachedAreaRepository.SelectByIdAsync(id);

        if (attachedArea is null)
            throw new LocationTrackerException(404, "Attached Area is not found");

        return _mapper.Map<AttachedAreaForResultDto>(attachedArea);
    }
}
