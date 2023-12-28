using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;

namespace dotNet_backend.Services.CoachService;

public class CoachService : ICoachService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IClubRepository _clubRepository;

    public CoachService(ICoachRepository coachRepository, IClubRepository clubRepository)
    {
        _coachRepository = coachRepository;
        _clubRepository = clubRepository;
    }
    
}