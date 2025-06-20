using AutoMapper;
using OT.Assessment.Application.Models.DTOs.Game;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Application.Mappings
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDto, Game>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GameCode, opt => opt.MapFrom(src => src.GameCode));
        }
    }
}
