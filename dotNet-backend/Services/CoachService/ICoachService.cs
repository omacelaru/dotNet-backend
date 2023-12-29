using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;

namespace dotNet_backend.Services.CoachService;

public interface ICoachService
{
    Task<Club> AddCoachToClubAsync(string coachUserName, Club club);
    Task<IEnumerable<Coach>> GetAllCoachesAsync();
    Task<Coach> GetCoachByIdAsync(Guid id);
}