using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Club.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.AthleteService;

public interface IAthleteService
{
    Task<ActionResult<IEnumerable<AthleteResponseDto>>> GetAllAthletesAsync();
    Task<ActionResult<AthleteResponseDto>> GetAthleteByUsernameAsync(string username);
}