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
    public async Task<Coach> FindByUsernameAsync(string coachUsername)
    {
        return await _table.FirstOrDefaultAsync(c => c.Username == coachUsername);
    }
    public async Task<IEnumerable<Coach>> FindAllCoachesAsync()
    {
        return await _table
            .Include(c => c.Athletes)
            .Include(c => c.Club)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Coach> FindCoachByIdAsync(Guid id)
    {
        return await _table
            .Include(c => c.Athletes)
            .Include(c => c.Club)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Coach> FindCoachByUsernameAsync(string username)
    {
        return await _table
            .Include(c => c.Athletes)
            .Include(c => c.Club)
            .FirstOrDefaultAsync(c => c.Username == username);
    }
}