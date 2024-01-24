using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Request;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.RequestRepository;
using dotNet_backend.Repositories.UserRepository;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.CoachService;

public class CoachService : ICoachService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IAthleteRepository _athleteRepository;

    public CoachService(ICoachRepository coachRepository, IClubRepository clubRepository, IAthleteRepository athleteRepository)
    {
        _coachRepository = coachRepository;
        _clubRepository = clubRepository;
        _athleteRepository = athleteRepository;
    }
    
    public async Task<IEnumerable<Coach>> GetAllCoachesAsync()
    {
        return await _coachRepository.FindAllCoachesAsync();
    }

    public async Task<Coach> GetCoachByIdAsync(Guid id)
    {
        return await _coachRepository.FindCoachByIdAsync(id);
    }
    
    public async Task<Coach> GetCoachByUserNameAsync(string username)
    {
        return await _coachRepository.FindCoachByUserNameAsync(username);
    }

    public async Task AddAthleteToCoach(string athleteUsername, string coachUsername)
    {
        var athlete = await _athleteRepository.FindAthleteByUserNameAsync(athleteUsername);
        var coach = await _coachRepository.FindCoachByUserNameAsync(coachUsername);
        if (athlete == null || coach == null)
        {
            throw new NotFoundException("Athlete or coach not found");
        }
        athlete.Coach = coach;
        await _athleteRepository.SaveAsync();
    }

    public async Task UpdateCoachAsync(Coach newCoach)
    {
        _coachRepository.Update(newCoach);
        await _coachRepository.SaveAsync();
    }
    
}