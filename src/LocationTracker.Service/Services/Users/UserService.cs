using AutoMapper;
using LocationTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Exceptions;
using LocationTracker.Domain.Entities.Users;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.Commons.Helpers;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Service.Interfaces.Users;
using LocationTracker.Service.Commons.Extentions;

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
                .FirstOrDefaultAsync();

            if (user != null)
                throw new LocationTrackerException(409, "User already exists.");

            var hashedPassword = PasswordHelper.Hash(dto.Password);
            var mappedUser = _mapper.Map<User>(dto);
            mappedUser.Salt = hashedPassword.Salt;
            mappedUser.Password = hashedPassword.Hash;

            var createdUser = await _userRepository.InsertAsync(mappedUser);

            return _mapper.Map<UserForResultDto>(createdUser);
        }
        public async Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto passwordDto)
        {
            var user = await _userRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new LocationTrackerException(404, "User not found");

            // Verify the old password
            if (!PasswordHelper.Verify(passwordDto.OldPassword, user.Salt, user.Password))
                throw new LocationTrackerException(400, "Incorrect old password");

            // Verify the new password is different from the old one
            if (passwordDto.OldPassword == passwordDto.NewPassword)
                throw new LocationTrackerException(400, "New password must be different from the old one");

            // Hash the new password
            var newPasswordHash = PasswordHelper.Hash(passwordDto.NewPassword);

            // Update the user's password and updatedAt
            user.Password = newPasswordHash.Hash;
            user.Salt = newPasswordHash.Salt; // Update the user's salt with the new password's salt
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return true;
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
