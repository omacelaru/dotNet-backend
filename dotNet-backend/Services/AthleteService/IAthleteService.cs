using dotNet_backend.Models.Athlete;

namespace dotNet_backend.Services.AthleteService;

public interface IAthleteService
{
    Task<Athlete> GetAthleteByUserNameAsync(string username);
}