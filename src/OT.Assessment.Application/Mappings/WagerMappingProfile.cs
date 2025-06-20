using AutoMapper;
using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.Application.Mappings
{
    public class WagerMappingProfile : Profile
    {
        public WagerMappingProfile()
        {
            CreateMap<WagerDto, WagerMessage>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<WagerMessage, Wager>();
        }
    }
}
