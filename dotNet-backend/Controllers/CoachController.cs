using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Participation.DTO;
using dotNet_backend.Models.Request;
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
        
        [HttpGet("me/athletes")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<AthleteUsernameResponseDto>>> GetCoachAthletes() => 
            await _coachService.GetCoachAthletesByUsernameAsync(User.Identity.Name);
        
        [HttpGet("me/athletes/competition/{competitionId:guid}")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<ParticipationAthleteWithAwardsResponseDto>>> GetCoachAthletesForCompetition(Guid competitionId) => 
            await _coachService.GetAthletesForCompetitionByIdAndCoachUsernameAsync(competitionId, User.Identity.Name);

        [HttpGet("me/requests-join")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequests() => 
            await _coachService.GetCoachRequestsByUsernameAsync(User.Identity.Name, RequestType.AddAthleteToCoach);
        
        [HttpGet("me/requests-participate")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequestsForCompetition() => 
            await _coachService.GetCoachRequestsByUsernameAsync(User.Identity.Name, RequestType.AddAthleteToCompetition);
        
        [HttpPatch("me/requests-join/athlete/{athleteUsername}")]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<RequestInfoResponseDto>> UpdateRequestForAddAthleteToCoach(string athleteUsername,
            [FromBody] RequestStatusRequestDto requestStatusDto) =>
             await _requestService.UpdateRequestStatusForAddAthleteToCoachAsync(athleteUsername, User.Identity.Name, requestStatusDto.RequestStatus);
        
        [HttpPatch("me/requests-participate/athlete/{athleteUsername}/competition/{competitionId:guid}")]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<RequestInfoWithCompetitionResponseDto>> UpdateRequestForAddAthleteToCompetition(string athleteUsername,Guid competitionId,
            [FromBody] RequestStatusRequestDto requestStatusDto) =>
             await _requestService.UpdateRequestStatusForAddAthleteToCompetitionAsync(athleteUsername, User.Identity.Name,competitionId, requestStatusDto.RequestStatus);
        
    }
}