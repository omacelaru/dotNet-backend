using dotNet_backend.Models.Athlete;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.AthleteRepository;

public interface IAthleteRepository : IGenericRepository<Athlete>
{
    Task<Athlete> FindByUserNameAsync(string athleteUsername);
}