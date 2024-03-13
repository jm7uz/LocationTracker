using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.DTOs.Districts;
using LocationTracker.Service.Configurations;
using LocationTracker.Domain.Entities.Districts;
using LocationTracker.Service.Commons.Extentions;
using LocationTracker.Data.IRepositories.Districts;
using LocationTracker.Service.Interfaces.Districts;
using LocationTracker.Data.IRepositories.Regions;

namespace LocationTracker.Service.Services.Districts;

public class DistrictService : IDistrictService
{
    private readonly IMapper _mapper;
    private readonly IDistrictRepository _repository;
    private readonly IRegionRepository _regionRepository;

    public DistrictService(IMapper mapper,
                          IDistrictRepository repository,
                          IRegionRepository regionRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _regionRepository = regionRepository;
    }

    public async Task<DistrictForResultDto> AddAsync(DistrictForCreationDto dto)
    {
        var region = await _regionRepository.SelectByIdAsync(dto.RegionId);

        if (region is null)
            throw new LocationTrackerException(404, "Region not found");

        var district = await _repository.SelectAll()
             .Where(d => d.Name.ToLower() == dto.Name.ToLower())
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (district is not null)
            throw new LocationTrackerException(409, "District is already exist");

        var mapped = _mapper.Map<District>(dto);

        var result = await _repository.InsertAsync(mapped);
        return _mapper.Map<DistrictForResultDto>(result);
    }

    public async Task<DistrictForResultDto> ModifyAsync(int id, DistrictForUpdateDto dto)
    {
        var region = await _regionRepository.SelectByIdAsync(dto.RegionId);

        if (region is null)
            throw new LocationTrackerException(404, "Region is not found");

        var district = await _repository.SelectAll()
            .Where(d => d.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (district is null)
            throw new LocationTrackerException(404, "District is not found!");

        var mapped = _mapper.Map(dto, district);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(mapped);

        return _mapper.Map<DistrictForResultDto>(mapped);
    }
    public async Task<bool> RemoveAsync(int id)
    {
        var district = await _repository.SelectAll()
            .Where(d => d.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (district is null)
            throw new LocationTrackerException(404, "District is not found!");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<DistrictForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var districts = await _repository.SelectAll()
            .AsNoTracking()
            .ToPagedList<District, int>(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DistrictForResultDto>>(districts);
    }

    public async Task<DistrictForResultDto> RetrieveByIdAsync(int id)
    {
        var district = await _repository.SelectAll()
            .Where(d => d.Id == id)
            .FirstOrDefaultAsync();

        if (district is null)
            throw new LocationTrackerException(404, "District is not found!");

        return _mapper.Map<DistrictForResultDto>(district);
    }
}
