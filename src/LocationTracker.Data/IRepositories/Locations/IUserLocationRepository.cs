using LocationTracker.Domain.Entities.Locations;

namespace LocationTracker.Data.IRepositories.Locations;

public interface IUserLocationRepository : IRepository<UserLocation, long>
{
}
