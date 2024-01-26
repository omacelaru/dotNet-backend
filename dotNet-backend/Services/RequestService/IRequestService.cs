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
    Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetRequestsByUsernameAsync(string username);

    Task<ActionResult<RequestInfoResponseDto>> UpdateRequestStatusAsync(string coachUsername, string usernameAthlete,
        string requestStatus);

    Task<ActionResult<RequestInfoResponseDto>> CreateRequestAsync(string athleteUsername, string coachUsername,
        RequestType addAthleteToCoach);
}