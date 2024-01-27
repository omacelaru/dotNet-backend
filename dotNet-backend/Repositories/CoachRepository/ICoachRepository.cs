using dotNet_backend.Models.Coach;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.CoachRepository;

public interface ICoachRepository : IGenericRepository<Coach>
{
    Task<IEnumerable<Coach>> FindAllCoachesAsync();

    Task<Coach> FindCoachByIdAsync(Guid id);
    Task<Coach> FindCoachByUsernameAsync(string username);
}