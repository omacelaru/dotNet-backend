using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.CompetitionRepository;
using dotNet_backend.Repositories.ParticipationRepository;

namespace dotNet_backend.Services.RankService;

public class RankService : IRankService
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IClubRepository _clubRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IParticipationRepository _participationRepository;
    
    public RankService(IAthleteRepository athleteRepository, IClubRepository clubRepository, ICoachRepository coachRepository, ICompetitionRepository competitionRepository, IParticipationRepository participationRepository)
    {
        _athleteRepository = athleteRepository;
        _clubRepository = clubRepository;
        _coachRepository = coachRepository;
        _competitionRepository = competitionRepository;
        _participationRepository = participationRepository;
    }
    
}