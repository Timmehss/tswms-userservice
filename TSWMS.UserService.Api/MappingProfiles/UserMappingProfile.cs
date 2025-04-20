using AutoMapper;
using TSWMS.UserService.Api.Dto;
using TSWMS.UserService.Shared.Models;

namespace TSWMS.UserService.Api.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }
}
