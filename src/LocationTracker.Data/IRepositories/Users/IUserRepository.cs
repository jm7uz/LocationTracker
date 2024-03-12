using LocationTracker.Domain.Entities.Users;

namespace LocationTracker.Data.IRepositories.Users;

public interface IUserRepository : IRepository<User, long>
{
}
