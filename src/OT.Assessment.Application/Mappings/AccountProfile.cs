using AutoMapper;
using OT.Assessment.Application.Models.DTOs.Account;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>()
             .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId));

            CreateMap<AccountDto, Account>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Player, opt => opt.Ignore())
                .ForMember(dest => dest.AccountNumber, opt => opt.Ignore());

        }
    }
}
