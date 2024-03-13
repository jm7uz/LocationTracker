using AutoMapper;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Domain.Entities.Users;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using LocationTracker.Service.DTOs.Users;
namespace LocationTracker.Service.Mappers;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        //User
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserRoleModifyDto>().ReverseMap();
        CreateMap<User, UserPhoneNumberModifyDto>().ReverseMap();
        CreateMap<User, UserAttachAreaModifyDto>().ReverseMap();


        //AttachedArea
        CreateMap<AttachedArea, AttachedAreaForCreationDto>().ReverseMap();
        CreateMap<AttachedArea, AttachedAreaForResultDto>().ReverseMap();
        CreateMap<AttachedArea, AttachedAreaForUpdateDto>().ReverseMap();
    }
}
