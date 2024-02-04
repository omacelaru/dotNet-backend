using dotNet_backend.Models.Request.DTO;
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
        
        /// <summary>
        /// Create a request to join in a coach's list
        /// </summary>
        [HttpPost("join/coach/{coachUsername}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoResponseDto>> JoinCoach(string coachUsername) =>
            await _requestService.CreateRequestToJoinInCoachListAsync(User.Identity.Name, coachUsername);
        
        /// <summary>
        /// Create a request to participate in a competition
        /// </summary>
        [HttpPost("participate/competition/{competitionId:guid}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoWithCompetitionResponseDto>> ParticipateInCompetition(Guid competitionId) =>
            await _requestService.CreateRequestToParticipateInCompetitionAsync(User.Identity.Name, competitionId);
    }
}