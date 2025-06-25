using AutoMapper;
using OT.Assessment.Application.Models.DTOs.Wager;
using OT.Assessment.Domain.Entities;

namespace OT.Assessment.Application.Mappings
{
    public class WagerProfile : Profile
    {
        public WagerProfile()
        {
            CreateMap<WagerDto, WagerMessage>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));


            CreateMap<WagerDto, Wager>().ReverseMap();
        }
    }
}
