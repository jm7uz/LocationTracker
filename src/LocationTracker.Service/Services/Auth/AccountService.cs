using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Service.Commons.Helpers;
using LocationTracker.Service.Commons.Models;
using LocationTracker.Service.DTOs.Logins;
using LocationTracker.Service.Exceptions;
using LocationTracker.Service.Interfaces.Auths;
using Microsoft.EntityFrameworkCore;

public class AccountService : IAccountService
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public AccountService(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<TokenModel> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.SelectAll()
            .Where(a => a.Id == loginDto.Id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new LocationTrackerException(404, "User not found");

        var isPasswordCorrect = PasswordHelper.Verify(loginDto.Password, user.Salt, user.Password);

        if (!isPasswordCorrect)
            throw new LocationTrackerException(404, "Incorrect password");

        return _authService.GenerateToken(user);
    }
}
