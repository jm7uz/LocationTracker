using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Domain.Entities.Users;
namespace LocationTracker.Data.Repositories.Users;

public class UserRepository : Repository<User, long>, IUserRepository
{
    public UserRepository(LocationTrackerDbContext context) :
        base(context)
    { 
    }
}
