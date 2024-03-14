using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.DTOs.Regions;
using LocationTracker.Service.Configurations;
using LocationTracker.Domain.Entities.Regions;
using LocationTracker.Data.IRepositories.Regions;
using LocationTracker.Service.Commons.Extentions;
using LocationTracker.Service.Interfaces.Regions;

namespace LocationTracker.Service.Services.Regions;

public class RegionService : IRegionService
{
    private readonly IMapper _mapper;
    private readonly IRegionRepository _repository;

    public RegionService(IMapper mapper,
                          IRegionRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<RegionForResultDto> AddAsync(RegionForCreationDto dto)
    {
        var region = await _repository.SelectAll()
             .Where(r => r.Name.ToLower() == dto.Name.ToLower())
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (region is not null)
            throw new LocationTrackerException(409, "Region is already exist!");

        var mapped = _mapper.Map<Region>(dto);

        var result = await _repository.InsertAsync(mapped);
        return _mapper.Map<RegionForResultDto>(result);
    }

    public async Task<RegionForResultDto> ModifyAsync(int id, RegionForUpdateDto dto)
    {
        var region = await _repository.SelectAll()
            .Where(r => r.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (region is null)
            throw new LocationTrackerException(404, "Region is not found!");

        var mapped = _mapper.Map(dto, region);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(mapped);

        return _mapper.Map<RegionForResultDto>(mapped);
    }
    public async Task<bool> RemoveAsync(int id)
    {
        var region = await _repository.SelectAll()
            .Where(r => r.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (region is null)
            throw new LocationTrackerException(404, "Region is not found!");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<RegionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var regions = await _repository.SelectAll()
            .AsNoTracking()
            .ToPagedList<Region, int>(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<RegionForResultDto>>(regions);
    }

    public async Task<RegionForResultDto> RetrieveByIdAsync(int id)
    {
        var region = await _repository.SelectAll()
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();

        if (region is null)
            throw new LocationTrackerException(404, "Region is not found!");

        return _mapper.Map<RegionForResultDto>(region);
    }
}
