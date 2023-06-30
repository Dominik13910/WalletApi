using AutoMapper;
using webapi.Dto.Users;
using webapi.Dto.Wallet;
using webapi.Models;

namespace webapi.Mappers
{
    public class WalletMappingProfile : Profile

    {
        public WalletMappingProfile()
        {
            CreateMap<Wallet, WalletDto>()
          .ForMember(
                    dest => dest.WalletId,
                    opt => opt.MapFrom(src => $"{src.WalletId}")
                )
                .ForMember(
                    dest => dest.Pln,
                    opt => opt.MapFrom(src => $"{src.Pln}")
                )
              .ForMember(
                    dest => dest.BitCoin,
                    opt => opt.MapFrom(src => $"{src.BitCoin}")
                )
              .ForMember(
                    dest => dest.Ether,
                    opt => opt.MapFrom(src => $"{src.Ether}")
                );


            CreateMap<CreateWalletDto, Wallet>();

        }


    }
    
    
}
