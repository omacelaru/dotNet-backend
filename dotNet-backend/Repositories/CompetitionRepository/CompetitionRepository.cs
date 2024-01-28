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
            .Include(c => c.Participations)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Competition> CreateCompetitionAsync(Competition competition)
    {
        await _table.AddAsync(competition);
        await _applicationDbContext.SaveChangesAsync();
        return competition;
    }
}