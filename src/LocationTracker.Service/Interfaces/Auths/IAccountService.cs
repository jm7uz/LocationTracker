using LocationTracker.Service.Commons.Models;
using LocationTracker.Service.DTOs.Logins;

namespace LocationTracker.Service.Interfaces.Auths;

public interface IAccountService
{
    public Task<TokenModel> LoginAsync(LoginDto loginDto);
}
