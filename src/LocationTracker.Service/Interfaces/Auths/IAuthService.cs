using LocationTracker.Domain.Entities.Users;
using LocationTracker.Service.Commons.Models;

namespace LocationTracker.Service.Interfaces.Auths;

public interface IAuthService
{
    public TokenModel GenerateToken(User users);

}
