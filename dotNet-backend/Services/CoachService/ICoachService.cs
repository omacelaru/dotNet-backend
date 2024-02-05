using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Participation.DTO;
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
    Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequestsByUsernameAsync(string coachUsername, RequestType requestType);
    Task<ActionResult<IEnumerable<AthleteUsernameResponseDto>>> GetCoachAthletesByUsernameAsync(string coachUsername);
    Task<ActionResult<IEnumerable<ParticipationAthleteWithAwardsResponseDto>>> GetAthletesForCompetitionByIdAndCoachUsernameAsync(Guid competitionId, string coachUsername);
}