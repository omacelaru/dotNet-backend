using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Services.RequestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestController(IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService _requestService = requestService;
        
        [HttpPost("join/coach/{coachUsername}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoResponseDto>> JoinCoach(string coachUsername) =>
            await _requestService.CreateRequestToJoinInCoachListAsync(User.Identity.Name, coachUsername);
        
        [HttpPost("participate/competition/{competitionId:guid}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoWithCompetitionResponseDto>> ParticipateInCompetition(Guid competitionId) =>
            await _requestService.CreateRequestToParticipateInCompetitionAsync(User.Identity.Name, competitionId);
    }
}