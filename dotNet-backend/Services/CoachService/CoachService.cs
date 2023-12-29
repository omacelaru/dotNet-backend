using dotNet_backend.Data.Exceptions;
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

    public async Task<Club> AddCoachToClubAsync(string coachUserName,Club club)
    {
        var coach = await _coachRepository.FindByUserNameAsync(coachUserName);
        if(coach.ClubId.HasValue)
            throw new CoachAlreadyHasAClub();
        var clubToAdd = await _clubRepository.FindByIdAsync(club.Id);
        if(clubToAdd == null)
            throw new Exception();
        clubToAdd.Coaches = new List<Coach>() { coach };
        coach.ClubId = clubToAdd.Id;
        _coachRepository.Update(coach);
        await _coachRepository.SaveAsync();
        return clubToAdd;
    }
    
    public async Task<IEnumerable<Coach>> GetAllCoachesAsync()
    {
        return await _coachRepository.GetAllAsync();
    }

    public async Task<Coach> GetCoachByIdAsync(Guid id)
    {
        return await _coachRepository.FindByIdAsync(id);
    }
}