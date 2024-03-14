using LocationTracker.Service.DTOs.Logins;
using LocationTracker.Service.Interfaces.Auths;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LocationTracker.Api.Controllers.Common;

[EnableRateLimiting("fixed")]
public class AuthController : BaseController
{
    private readonly IAccountService accountService;

    public AuthController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpPost]
    [Route("login")]
    public async ValueTask<IActionResult> login([FromBody] LoginDto loginDto)
        => Ok(await accountService.LoginAsync(loginDto));
}
