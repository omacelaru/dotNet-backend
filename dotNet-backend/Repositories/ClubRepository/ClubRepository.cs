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
                .Include(c => c.Coach)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<Club> FindClubByIdAsync(Guid clubId)
        {
            return await _table
                .Include(c => c.Coach)
                .FirstOrDefaultAsync(c => c.Id == clubId);
        }
    }
}