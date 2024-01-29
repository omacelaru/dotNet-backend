using dotNet_backend.Models.Participation;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.ParticipationRepository;

public class ParticipationRepository : IParticipationRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Participation> _table;

    public ParticipationRepository(ApplicationDbContext context)
    {
        _applicationDbContext = context;
        _table = _applicationDbContext.Set<Participation>();
    }
    public async Task CreateAsync(Participation participation)
    {
        await _table.AddAsync(participation);
    }
    public async Task<bool> SaveAsync()
    {
        return await _applicationDbContext.SaveChangesAsync() > 0;
    }
    public void Update(Participation participation)
    {
        _table.Update(participation);
    }
    public async Task<IEnumerable<Participation>> FindAllAthletesParticipatingInACompetitionByIdAsync(Guid id)
    {
        return await _table
            .IncludeAll()
            .Where(p => p.CompetitionId == id).ToListAsync();
    }
    
    public async Task<Participation> FindParticipationByAthleteIdAndCompetitionId(Guid athleteId, Guid competitionId)
    {
        return await _table
            .IncludeAll()
            .FirstOrDefaultAsync(p => p.AthleteId == athleteId && p.CompetitionId == competitionId);
    }
    
    public async Task<IEnumerable<Participation>> FindAllAthletesForCompetitionByIdAndCoachUsernameAsync(Guid competitionId, string coachUsername)
    {
        return await _table
            .IncludeAll()
            .Where(p => p.CompetitionId == competitionId && p.Athlete.Coach.Username == coachUsername).ToListAsync();
    }
    
}

public static class ParticipationRepositoryExtensions
{
    public static IQueryable<Participation> IncludeAll(this IQueryable<Participation> query)
    {
        return query
            .Include(p => p.Athlete)
            .Include(p => p.Athlete.Coach)
            .Include(p => p.Competition)
            .AsNoTracking();
    }
}