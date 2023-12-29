using dotNet_backend.Models.Athlete;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.AthleteRepository;

public class AthleteRepository : GenericRepository<Athlete>, IAthleteRepository
{
    public AthleteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Athlete> FindByUserNameAsync(string athleteUsername)
    {
        return await _table.FirstOrDefaultAsync(a => a.Username == athleteUsername);
    }
}