using AutoMapper;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Data.Repositories.Users;
using LocationTracker.Domain.Entities.Regions;
using LocationTracker.Domain.Entities.Users;
using LocationTracker.Domain.Enums;
using LocationTracker.Service.Commons.Extentions;
using LocationTracker.Service.Commons.Helpers;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.DTOs.Regions;
using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Service.Services.Users
{
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

            var hasherResult = PasswordHelper.Hash(dto.Password);
            var mappedUser = _mapper.Map<User>(dto);
            mappedUser.Salt = hasherResult.Salt.ToString();
            mappedUser.Password = hasherResult.Hash;

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
                    var users = await _userRepository
                    .SelectAll()
                    .AsNoTracking()
                    .ToPagedList<User, long>(@params)
                    .ToListAsync();


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
