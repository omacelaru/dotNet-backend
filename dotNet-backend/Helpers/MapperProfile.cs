using AutoMapper;
using Azure.Core;
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Competition;
using dotNet_backend.Models.Competition.DTO;
using dotNet_backend.Models.Participation;
using dotNet_backend.Models.Participation.DTO;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.User;
using dotNet_backend.Models.User.DTO;

namespace dotNet_backend.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<RegisterDto, Athlete>();
            CreateMap<RegisterDto, Coach>();
            CreateMap<RegisterDto, User>();
            CreateMap<ClubRequestDto, Club>();

            CreateMap<Coach, CoachResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.Name))
                .ForMember(dest => dest.AthletesNames,
                                       opt => opt.MapFrom(src => src.Athletes.Select(athlete => athlete.Name)));

            CreateMap<Athlete, AthleteResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CoachName, opt => opt.MapFrom(src => src.Coach.Name));

            CreateMap<Club, ClubResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CoachName,
                    opt => opt.MapFrom(src => src.Coach.Name));

            CreateMap<RequestInfo, RequestInfoResponseDto>();

            CreateMap<CompetitionRequestDto, Competition>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToDateTime(new TimeOnly(9, 0, 0))))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToDateTime(new TimeOnly(9,0,0))));

            CreateMap<Competition, CompetitionNameResponseDto>();
            CreateMap<Competition, CompetitionResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.Date))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.Date))
                .ForMember(dest => dest.NumberOfParticipants, opt => opt.MapFrom(src => src.Participations.Count))
                .ForMember(dest => dest.DayLeft, opt => opt.MapFrom(src => (src.EndDate - DateTime.Now).Days));

            CreateMap<Athlete, AthleteUsernameResponseDto>();
        }
    }
}