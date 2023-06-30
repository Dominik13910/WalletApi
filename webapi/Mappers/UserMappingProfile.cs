using AutoMapper;
using webapi.Dto.Users;
using webapi.Models;

namespace webapi.Mappers
{
    public class UserMappingProfile : Profile

    {
        public UserMappingProfile() 
        {
            CreateMap<User, UserDto>()
          .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{src.Id}")
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                );


            CreateMap<CreateUserDto, User>();

        }


    }
}
