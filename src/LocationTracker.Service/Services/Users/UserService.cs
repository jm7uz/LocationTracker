using AutoMapper;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Data.Repositories.Users;
using LocationTracker.Domain.Entities.Users;
using LocationTracker.Domain.Enums;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.Id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is not null)
            throw new LocationTrackerException(409, "User Area is already exist.");

        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.CreatedAt = DateTime.UtcNow;
        mappedUser.Salt = ".";
        mappedUser.Role = Role.User;

        var createdUser = await _userRepository.InsertAsync(mappedUser);

        return _mapper.Map<UserForResultDto>(createdUser);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new LocationTrackerException(409, "User not found.");

        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.UpdatedAt = DateTime.UtcNow;

        var updateUser = await _userRepository.UpdateAsync(mappedUser);

        return _mapper.Map<UserForResultDto>(updateUser);

    }

    public async Task<UserForResultDto> ModifyAttachAreaAsync(long id, int attachAreaId)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new LocationTrackerException(409, "User not found.");

        user.AttachedAreaId = attachAreaId;
        user.UpdatedAt = DateTime.UtcNow;

        var updateUser = await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserForResultDto>(updateUser);
    }

        public async Task<UserForResultDto> ModifyRoleAsync(long id, Role roleId)
        {
            var user = await _userRepository.SelectAll()
                .Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new LocationTrackerException(409, "User not found.");

            user.Role = roleId;
            user.UpdatedAt = DateTime.UtcNow;

            var updateUser = await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserForResultDto>(updateUser);
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var user = await _userRepository.SelectAll()
                .Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new LocationTrackerException(409, "User not found.");

            var isDeleted = await _userRepository.DeleteAsync(id);
            return isDeleted;
        }

        public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var users = await _userRepository.SelectAll()
               .Where(u => u.Id > 0)
               .AsNoTracking()
               .FirstOrDefaultAsync();

            if (users is null)
                throw new LocationTrackerException(409, "User empty");

            return _mapper.Map<IEnumerable<UserForResultDto>>(users);
        }

        public async Task<UserForResultDto> RetrieveByIdAsync(long id)
        {
            var user = await _userRepository.SelectAll()
                .Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new LocationTrackerException(409, "User not found.");

            return _mapper.Map<UserForResultDto>(user);
        }

        public async Task<IEnumerable<UserForResultDto>> SearchForUser(string smthing)
        {
        if (int.TryParse(smthing, out int userId)) // Check if input is integer
        {
            var users = _userRepository.SelectAll().Where(u => u.Id.ToString().StartsWith(smthing)).ToList();

            return await _mapper.Map<Task<IEnumerable<UserForResultDto>>>(users);
        }
        else // If input is not an integer, assume it's a name
        {
            var users = _userRepository.SelectAll().Where(u => u.FirstName.StartsWith(smthing)).ToList();

            return await _mapper.Map<Task<IEnumerable<UserForResultDto>>>(users);
        }

        throw new LocationTrackerException(404, "Any User is not found with this info");
    }
}

