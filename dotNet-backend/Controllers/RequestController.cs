using AutoMapper;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Services.AthleteService;
using dotNet_backend.Services.ClubService;
using dotNet_backend.Services.CoachService;
using dotNet_backend.Services.RequestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<RegisterController> _logger;

        public RequestController(IRequestService requestService, ILogger<RegisterController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }
        
        [HttpPost("join/{coachUsername}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoResponseDto>> JoinCoach(string coachUsername) =>
            await _requestService.CreateRequestAsync(User.Identity.Name, coachUsername,
                RequestType.AddAthleteToCoach);
    }
}