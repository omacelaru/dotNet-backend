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

        // Add custom methods here

        public async Task<IEnumerable<Club>> FindAllClubsAsync()
        {
            return await _table
                .Include(c => c.Coach)
                .ToListAsync();
        }
    }
}