using AutoMapper;
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;

namespace dotNet_backend.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<AthleteRegisterDto, Athlete>();
            CreateMap<CoachRegisterDto, Coach>();
            CreateMap<ClubRequestDto, Club>();

            CreateMap<Coach, CoachResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.Name));

            CreateMap<Athlete, AthleteResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CoachName, opt => opt.MapFrom(src => src.Coach.Name));

            CreateMap<Club, ClubResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CoachesNames, opt => opt.MapFrom(src => src.getCoachesNames()));
        }
    }
}
