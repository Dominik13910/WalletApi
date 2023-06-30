using AutoMapper;
using webapi.Dto.Roles;
using webapi.Models;

namespace webapi.Mappers
{
    public class RolesMappingProfile : Profile
    {
        public RolesMappingProfile()
        {
            CreateMap<Roles, RolesDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{src.RolesID}")
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.RoleName}")
                );


            CreateMap<CreateRolesDto, Roles>();

        }
    }
}
