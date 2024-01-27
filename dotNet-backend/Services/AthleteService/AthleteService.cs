using dotNet_backend.Models.Athlete;
using dotNet_backend.Repositories.AthleteRepository;

namespace dotNet_backend.Services.AthleteService;

public class AthleteService : IAthleteService
{
    private readonly IAthleteRepository _athleteRepository;

    public AthleteService(IAthleteRepository athleteRepository)
    {
        _athleteRepository = athleteRepository;
    }

    public async Task<Athlete> GetAthleteByUsernameAsync(string username)
    {
        return await _athleteRepository.FindSingleOrDefaultAsync(a => a.Username == username);
    }
}