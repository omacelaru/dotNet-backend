using dotNet_backend.Models.Athlete;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.AthleteRepository;

public class AthleteRepository : GenericRepository<Athlete>, IAthleteRepository
{
    public AthleteRepository(ApplicationDbContext context) : base(context)
    {
    }
}