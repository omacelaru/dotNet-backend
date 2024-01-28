using dotNet_backend.Models.Competition;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.CompetitionRepository;

public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
{
    public CompetitionRepository(ApplicationDbContext context) : base(context)
    {
    }
}