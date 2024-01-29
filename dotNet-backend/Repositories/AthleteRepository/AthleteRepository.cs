using dotNet_backend.Models.Athlete;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.AthleteRepository;

public class AthleteRepository : GenericRepository<Athlete>, IAthleteRepository
{
    public AthleteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Athlete>> FindAllAthletesAsync()
    {
        return await _table
            .IncludeAll()
            .ToListAsync();
    }

    public async Task<Athlete> FindAthleteByUsernameAsync(string username)
    {
        return await _table
            .IncludeAll()
            .FirstOrDefaultAsync(a => a.Username == username);
    }
    
    public async Task<IEnumerable<Athlete>> FindAllAthletesAssignedToCoachUsernameAsync(string coachUsername)
    {
        return await _table
            .IncludeAll()
            .Where(a => a.Coach.Username == coachUsername)
            .ToListAsync();
    }
}

public static class AthleteRepositoryExtensions
{
    public static IQueryable<Athlete> IncludeAll(this IQueryable<Athlete> query)
    {
        return query
            .Include(a => a.Coach)
            .Include(a => a.Participations)
            .AsNoTracking();
    }
}