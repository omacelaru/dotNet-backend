using dotNet_backend.Models.Club;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.ClubRepository
{
    public class ClubRepository : GenericRepository<Club>, IClubRepository
    {
        public ClubRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Club>> FindAllClubsAsync()
        {
            return await _table
                .IncludeAll() 
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<Club> FindClubByIdAsync(Guid clubId)
        {
            return await _table
                .IncludeAll()
                .FirstOrDefaultAsync(c => c.Id == clubId);
        }
    }
    
    public static class ClubRepositoryExtensions
    {
        public static IQueryable<Club> IncludeAll(this IQueryable<Club> query)
        {
            return query
                .Include(c => c.Coach)
                .AsNoTracking();
        }
    }
}