using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Configurations;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.Interfaces.Locations;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;

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

        if (attachedArea is not null)
            throw new LocationTrackerException(404, "Attached Area is not found");

        var mappedAttachedArea = _mapper.Map(dto, attachedArea);
        mappedAttachedArea.UpdatedAt = DateTime.UtcNow;

        await _attachedAreaRepository.UpdateAsync(mappedAttachedArea);

        return _mapper.Map<AttachedAreaForResultDto>(mappedAttachedArea);
    }

    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AttachedAreaForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<AttachedAreaForResultDto> RetrieveByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
