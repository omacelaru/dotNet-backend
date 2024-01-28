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
    Task<ActionResult<RequestInfoResponseDto>> UpdateRequestStatusAsync(string requestedByUsername, string assignedToUser,
        string requestStatus, RequestType requestType);

    Task<ActionResult<RequestInfoResponseDto>> CreateRequestAsync(string requestedByUsername, string assignedToUser,
        RequestType requestType);
}