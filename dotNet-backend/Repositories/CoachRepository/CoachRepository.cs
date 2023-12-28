using dotNet_backend.Models.Coach;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.CoachRepository;

public class CoachRepository : GenericRepository<Coach>, ICoachRepository
{
    public CoachRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add custom methods here
    public async Task<Coach> FindByUserNameAsync(string coachUsername)
    {
        return await _table.FirstOrDefaultAsync(c => c.Username == coachUsername);
    }
}