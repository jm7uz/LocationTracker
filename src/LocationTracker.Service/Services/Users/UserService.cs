using LocationTracker.Service.DTOs.Users;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.Interfaces.Users;

namespace LocationTracker.Service.Services.Users
{
    public class UserService : IUserService
    {
        public Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> ModifyAttachAreaAsync(long id, UserAttachAreaModifyDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> ModifyPhoneNumberAsync(long id, UserPhoneNumberModifyDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> ModifyRoleAsync(long id, UserRoleModifyDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> RetrieveByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
