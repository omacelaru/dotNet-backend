using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.CoachService;

public interface ICoachService
{
    Task<ActionResult<IEnumerable<CoachResponseDto>>> GetAllCoachesAsync();
    Task<ActionResult<CoachResponseDto>> GetCoachByIdAsync(Guid id);
    Task<ActionResult<CoachResponseDto>> GetCoachByUsernameAsync(string coachUsername);
    Task AddAthleteToCoachAsync(string athleteUsername, string coachUsername);
    Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequestsByUsernameAsync(string coachUsername);
}