using Microsoft.EntityFrameworkCore;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.DTOs.Logins;
using LocationTracker.Service.Commons.Models;
using LocationTracker.Service.Commons.Helpers;
using LocationTracker.Service.Interfaces.Auths;
using LocationTracker.Data.IRepositories.Users;


namespace LocationTracker.Service.Services.Auth;

public class AccountService : IAccountService
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;
    public AccountService(
        IAuthService authService,
        IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }
    public async Task<TokenModel> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.SelectAll()
                .Where(a => a.Password == loginDto.Password)
                .Include(a => a.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        if (user is null)
            throw new LocationTrackerException(404, "Id raqam yoki parol xato kiritildi!");

        var hasherResult = PasswordHelper.Verify(loginDto.Password, user.Salt, user.Password);

        if (hasherResult == false)
            throw new LocationTrackerException(404, "Id raqam yoki parol xato kiritildi!");

        return _authService.GenerateToken(user);
    }
}
