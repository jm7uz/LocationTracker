using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Domain.Entities.Locations;

namespace LocationTracker.Data.Repositories.Locations;

public class AttachedAreaRepository : Repository<AttachedArea, int>, IAttachedAreaRepository
{
    public AttachedAreaRepository(LocationTrackerDbContext context) : 
        base(context)
    {

    }
}
