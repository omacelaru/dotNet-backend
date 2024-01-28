using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Services.CoachService;
using dotNet_backend.Services.RequestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoachController(ICoachService coachService, IRequestService requestService) : ControllerBase
    {
        private readonly ICoachService _coachService = coachService;
        private readonly IRequestService _requestService = requestService;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoachResponseDto>>> GetAllCoaches() =>
             await _coachService.GetAllCoachesAsync();

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachById(Guid id) => 
            await _coachService.GetCoachByIdAsync(id);

        [HttpGet("{coachUsername}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachByUsername(string coachUsername) =>
            await _coachService.GetCoachByUsernameAsync(coachUsername);

        [HttpGet("me")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<CoachResponseDto>> GetMeByUsername() => 
            await _coachService.GetCoachByUsernameAsync(User.Identity.Name);

        [HttpGet("me/requests")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequests() => 
            await _coachService.GetCoachRequestsByUsernameAsync(User.Identity.Name);
        
        [HttpPatch("me/requests/{athleteUsername}")]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<RequestInfoResponseDto>> UpdateRequestForAddAthleteToCoach(string athleteUsername,
            [FromBody] RequestStatusRequestDto requestStatusDto) =>
             await _requestService.UpdateRequestStatusAsync(athleteUsername, User.Identity.Name, requestStatusDto.RequestStatus,
                RequestType.AddAthleteToCoach);
    }
}