using dotNet_backend.Models.Competition;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.CompetitionRepository;

public interface ICompetitionRepository : IGenericRepository<Competition>
{
    Task<IEnumerable<Competition>> GetAllCompetitions();
    Task<Competition> CreateCompetitionAsync(Competition competition);
}