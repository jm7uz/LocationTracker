﻿using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.Interfaces.Users;
using AutoMapper;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Data.Repositories.Users;
using LocationTracker.Data.Repositories.Locations;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using LocationTracker.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using LocationTracker.Domain.Entities.Users;
using LocationTracker.Domain.Enums;

namespace LocationTracker.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;

        public UserService(IMapper mapper, UserRepository userRepository)
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
    }
}
