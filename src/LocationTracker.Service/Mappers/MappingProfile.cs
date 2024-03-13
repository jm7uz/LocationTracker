using AutoMapper;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using LocationTracker.Domain.Entities.Districts;
using LocationTracker.Domain.Entities.Regions;
using LocationTracker.Service.DTOs.Districts;
using LocationTracker.Service.DTOs.Regions;
using LocationTracker.Service.DTOs.Locations.LocationReports;
namespace LocationTracker.Service.Mappers;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        //AttachedArea
        CreateMap<AttachedArea, AttachedAreaForCreationDto>();
        CreateMap<AttachedArea, AttachedAreaForResultDto>();
        CreateMap<AttachedArea, AttachedAreaForUpdateDto>();

        // LocationReport
        CreateMap<locationReport, LocationReportForCreationDto>().ReverseMap();
        CreateMap<locationReport, LocationReportForUpdateDto>().ReverseMap();
        CreateMap<locationReport, LocationReportForResultDto>().ReverseMap();
    }

}
