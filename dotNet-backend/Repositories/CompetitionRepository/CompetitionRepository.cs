using dotNet_backend.Models.Competition;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.CompetitionRepository;

public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
{
    public CompetitionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Competition>> GetAllCompetitions()
    {
        return await _table
            .IncludeAll()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Competition> CreateCompetitionAsync(Competition competition)
    {
        await _table.AddAsync(competition);
        await _applicationDbContext.SaveChangesAsync();
        return competition;
    }

    public async Task<Competition> FindCompetitionByIdAsync(Guid id)
    {
        return await _table
            .IncludeAll()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}

public static class CompetitionRepositoryExtensions
{
    public static IQueryable<Competition> IncludeAll(this IQueryable<Competition> query)
    {
        return query
            .Include(c => c.Participations);
    }
}