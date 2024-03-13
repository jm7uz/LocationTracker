using LocationTracker.Data.IRepositories.Districts;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Data.IRepositories.Regions;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Data.Repositories.Districts;
using LocationTracker.Data.Repositories.Locations;
using LocationTracker.Data.Repositories.Regions;
using LocationTracker.Data.Repositories.Users;
using LocationTracker.Service.Interfaces.Districts;
using LocationTracker.Service.Interfaces.Regions;
using LocationTracker.Service.Services.Districts;
using LocationTracker.Service.Services.Regions;

namespace LocationTracker.Api.Extentions;

public static class ServiceExtentions
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // Users
        services.AddScoped<IUserRepository, UserRepository>();

        //Regions
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<IRegionService, RegionService>();

        //Districts
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<IDistrictService, DistrictService>();

        //Locations
        services.AddScoped<IAttachedAreaRepository, AttachedAreaRepository>();
        services.AddScoped<ILocationReportRepository, LocationReportRepository>();
        services.AddScoped<IPointLocationRepository, PointLocationRepository>();
        services.AddScoped<IUserLocationRepository, UserLocationRepository>();
    }
}
