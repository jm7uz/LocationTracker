using LocationTracker.Data.IRepositories.Districts;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Data.IRepositories.Regions;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Data.Repositories.Districts;
using LocationTracker.Data.Repositories.Locations;
using LocationTracker.Data.Repositories.Regions;
using LocationTracker.Data.Repositories.Users;
using LocationTracker.Service.Interfaces.Locations;
using LocationTracker.Service.Services.Locations;
using LocationTracker.Service.Interfaces.Districts;
using LocationTracker.Service.Interfaces.Regions;
using LocationTracker.Service.Services.Districts;
using LocationTracker.Service.Services.Regions;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LocationTracker.Service.Interfaces.Users;
using LocationTracker.Service.Services.Users;
using LocationTracker.Service.Interfaces.Auths;
using LocationTracker.Service.Services.Auth;

namespace LocationTracker.Api.Extentions;

public static class ServiceExtentions
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // Users
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        // Auth
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountService, AccountService>();
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
        services.AddScoped<IPointLocationService, PointLocationService>();
        services.AddScoped<IAttachedAreaService, AttachedAreaService>();
        services.AddScoped<ILocationReportService, LocationReportService>();
        services.AddScoped<ILocationCheckerService, LocationCheckerService>();
        services.AddScoped<IUserLocationRepository, UserLocationRepository>();
        services.AddScoped<IUserLocationService, UserLocationService>();
    }

    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationTracker.Server.Api", Version = "v1" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                ValidAudience = configuration["Jwt:Audience"],
                RequireExpirationTime = true
            };
        });
    }
}
