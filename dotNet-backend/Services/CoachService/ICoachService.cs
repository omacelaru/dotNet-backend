using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Request;

namespace dotNet_backend.Services.CoachService;

public interface ICoachService
{
    Task<IEnumerable<Coach>> GetAllCoachesAsync();
    Task<Coach> GetCoachByIdAsync(Guid id);
    Task<Coach> GetCoachByUserNameAsync(string username);
    Task AddAthleteToCoach(string usernameAthlete, string usernameCoach);
    Task UpdateCoachAsync(Coach newCoach);
}