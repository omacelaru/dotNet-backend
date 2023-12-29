using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.UserRepository;

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
    
    public async Task<IEnumerable<Coach>> GetAllCoachesAsync()
    {
        return await _coachRepository.GetAllAsync();
    }

    public async Task<Coach> GetCoachByIdAsync(Guid id)
    {
        return await _coachRepository.FindByIdAsync(id);
    }
    
    public async Task<Coach> GetCoachByUserNameAsync(string username)
    {
        return await _coachRepository.FindByUserNameAsync(username);
    }
}