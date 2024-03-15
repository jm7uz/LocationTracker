using AutoMapper;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.Commons.Extentions;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Locations.UserLocations;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Interfaces.Locations;
using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Service.Services.Locations;

public class UserLocationService : IUserLocationService
{
    private readonly IMapper _mapper;
    private readonly IUserLocationRepository _repository;
    private readonly IUserRepository _userRepository;

    public UserLocationService(IMapper mapper,
                          IUserLocationRepository repository,
                          IUserRepository regionRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = regionRepository;
    }

    public async Task<UserLocationForResultDto> AddAsync(UserLocationForCreationDto dto)
    {
        var user = await _userRepository.SelectByIdAsync(dto.UserId);

        if (user is null)
            throw new LocationTrackerException(404, "User is not found");

        var userLocation = await _repository.SelectAll()
             .Where(ul => (ul.Longitude == dto.Longitude ) && (ul.Latitude == dto.Latitude))
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (userLocation is not null)
            throw new LocationTrackerException(409, "User Location is already exist");

        var mapped = _mapper.Map<UserLocation>(dto);

        var result = await _repository.InsertAsync(mapped);
        return _mapper.Map<UserLocationForResultDto>(result);
    }

    public async Task<UserLocationForResultDto> ModifyAsync(long id, UserLocationForUpdateDto dto)
    {
        var user = await _userRepository.SelectByIdAsync(dto.UserId);

        if (user is null)
            throw new LocationTrackerException(404, "User is not found");

        var userLocation = await _repository.SelectAll()
            .Where(ul => ul.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (userLocation is null)
            throw new LocationTrackerException(404, "User Location is not found!");

        var mapped = _mapper.Map(dto, userLocation);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(mapped);

        return _mapper.Map<UserLocationForResultDto>(mapped);
    }
    public async Task<bool> RemoveAsync(long id)
    {
        var userLocation = await _repository.SelectAll()
            .Where(ul => ul.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (userLocation is null)
            throw new LocationTrackerException(404, "User Location is not found!");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<UserLocationForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var userLocations = await _repository.SelectAll()
            .AsNoTracking()
            .ToPagedList<UserLocation, long>(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserLocationForResultDto>>(userLocations);
    }

    public async Task<UserLocationForResultDto> RetrieveByIdAsync(long id)
    {
        var userLocation = await _repository.SelectAll()
            .Where(ul => ul.Id == id)
            .FirstOrDefaultAsync();

        if (userLocation is null)
            throw new LocationTrackerException(404, "User Location is not found!");

        return _mapper.Map<UserLocationForResultDto>(userLocation);
    }
}
