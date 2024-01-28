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
            .Include(a => a.Coach)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Athlete> FindAthleteByUsernameAsync(string username)
    {
        return await _table
            .Include(a => a.Coach)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Username == username);
    }
    
    public async Task<IEnumerable<Athlete>> GetAllAthletesAsync()
    {
        return await _table
            .Include(a => a.Coach)
            .AsNoTracking()
            .ToListAsync();
    }
}