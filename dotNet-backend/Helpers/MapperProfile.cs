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
        }
    }
}
