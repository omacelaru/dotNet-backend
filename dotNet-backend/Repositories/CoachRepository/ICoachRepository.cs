using dotNet_backend.Models.Coach;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.CoachRepository;

public interface ICoachRepository : IGenericRepository<Coach>
{
    Task<Coach> FindByUserNameAsync(string coachUsername);
}