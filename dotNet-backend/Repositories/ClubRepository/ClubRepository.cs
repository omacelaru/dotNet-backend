using dotNet_backend.Models.Club;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.ClubRepository
{
    public class ClubRepository : GenericRepository<Club>, IClubRepository
    {
        public ClubRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Add custom methods here
    }
}