using AutoMapper.Internal;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Competition.DTO;

namespace dotNet_backend.Models.Participation.DTO
{

    public class ParticipationResponseDto
    {
        public CompetitionNameResponseDto Competition { get; set; }
        public IEnumerable<AthleteUsernameResponseDto>? Athletes { get; set; }

    }

    public class ParticipationAthleteWithAwardsResponseDto
    {
        public CompetitionNameResponseDto Competition { get; set; }
        public AthleteUsernameResponseDto Athlete { get; set; }
        public int? FirstPlace { get; set; }
        public int? SecondPlace { get; set; }
        public int? ThirdPlace { get; set; }
    }
}
