using AutoMapper;
using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.Application.Mappings
{
    public class WagerProfile : Profile
    {
        public WagerProfile()
        {
            CreateMap<WagerDto, WagerMessage>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
