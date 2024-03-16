
using LocationTracker.Domain.Entities.Users;
using LocationTracker.Service.Commons.Models;
using LocationTracker.Service.Interfaces.Auths;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocationTracker.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("Jwt");
    }

    public TokenModel GenerateToken(User user)
    {
        var claims = new[]
        {

            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName+ " " + user.LastName),
            new Claim("Password", user.Password),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(_configuration["Issuer"], _configuration["Audience"], claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Lifetime"])),
            signingCredentials: credentials);

        var result = new TokenModel();
        result.Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return result;
    }
}
