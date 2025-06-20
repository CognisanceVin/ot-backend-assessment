using AutoMapper;
using OT.Assessment.Application.Models.DTOs.Account;
using OT.Assessment.Application.Models.DTOs.Player;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Application.Mappings
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDto, Player>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
