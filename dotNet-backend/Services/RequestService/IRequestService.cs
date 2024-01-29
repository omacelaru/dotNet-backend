using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.RequestService;

public interface IRequestService
{
    Task<ActionResult<RequestInfoResponseDto>> CreateRequestToJoinInCoachListAsync(string athleteUsername, string coachUsername);
    Task<ActionResult<RequestInfoResponseDto>> UpdateRequestStatusForAddAthleteToCoachAsync(string athleteUsername, string coachUsername, string requestStatus);
    Task<ActionResult<RequestInfoWithCompetitionResponseDto>> CreateRequestToParticipateInCompetitionAsync(string athleteUsername, Guid competitionId);
    Task<ActionResult<RequestInfoWithCompetitionResponseDto>> UpdateRequestStatusForAddAthleteToCompetitionAsync(string athleteUsername, string coachUsername, Guid competitionId, string requestStatus);
}