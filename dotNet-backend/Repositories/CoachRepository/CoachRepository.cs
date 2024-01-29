using dotNet_backend.Models.Coach;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.CoachRepository;

public class CoachRepository : GenericRepository<Coach>, ICoachRepository
{
    public CoachRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Coach>> FindAllCoachesAsync()
    {
        return await _table
            .IncludeAll()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Coach> FindCoachByIdAsync(Guid id)
    {
        return await _table
            .IncludeAll()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Coach> FindCoachByUsernameAsync(string username)
    {
        return await _table
            .IncludeAll()
            .FirstOrDefaultAsync(c => c.Username == username);
    }
}

public static class CoachRepositoryExtensions
{
    public static IQueryable<Coach> IncludeAll(this IQueryable<Coach> query)
    {
        return query
            .Include(c => c.Athletes)
            .Include(c => c.Club)
            .AsNoTracking();
    }
}