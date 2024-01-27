using dotNet_backend.Models.Athlete;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.AthleteRepository;

public interface IAthleteRepository : IGenericRepository<Athlete>
{
    Task<IEnumerable<Athlete>> FindAllAthletesAsync();
    Task<Athlete> FindAthleteByUsernameAsync(string athleteUsername);
}