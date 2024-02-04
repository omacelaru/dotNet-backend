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
        
        /// <summary>
        /// Get all coaches
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoachResponseDto>>> GetAllCoaches() =>
             await _coachService.GetAllCoachesAsync();

        /// <summary>
        /// Get a coach by id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachById(Guid id) => 
            await _coachService.GetCoachByIdAsync(id);

        /// <summary>
        /// Get a coach by username
        /// </summary>
        /// <param name="coachUsername"></param>
        [HttpGet("{coachUsername}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachByUsername(string coachUsername) =>
            await _coachService.GetCoachByUsernameAsync(coachUsername);

        /// <summary>
        /// Get the current coach
        /// </summary>
        [HttpGet("me")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<CoachResponseDto>> GetMeByUsername() => 
            await _coachService.GetCoachByUsernameAsync(User.Identity.Name);
        
        /// <summary>
        /// Get the current coach's athletes
        /// </summary>
        [HttpGet("me/athletes")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<AthleteUsernameResponseDto>>> GetCoachAthletes() => 
            await _coachService.GetCoachAthletesByUsernameAsync(User.Identity.Name);
         
        /// <summary>
        /// Get the current coach's athletes participating in a competition
        /// </summary>
        /// <param name="competitionId"></param>
        [HttpGet("me/athletes/competition/{competitionId:guid}")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<ParticipationAthleteWithAwardsResponseDto>>> GetCoachAthletesForCompetition(Guid competitionId) => 
            await _coachService.GetAthletesForCompetitionByIdAndCoachUsernameAsync(competitionId, User.Identity.Name);

        /// <summary>
        /// Get enrollment requests made by athletes for the current coach
        /// </summary>
        [HttpGet("me/requests-join")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequests() => 
            await _coachService.GetCoachRequestsByUsernameAsync(User.Identity.Name, RequestType.AddAthleteToCoach);
        
        /// <summary>
        /// Get participation in a competition requests made by athletes for the current coach
        /// </summary>
        [HttpGet("me/requests-participate")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequestsForCompetition() => 
            await _coachService.GetCoachRequestsByUsernameAsync(User.Identity.Name, RequestType.AddAthleteToCompetition);
        
        /// <summary>
        /// Respond to an enrollment request made by an athlete for the current coach
        /// </summary>
        /// <param name="athleteUsername"></param>
        /// <param name="requestStatusDto"></param>
        [HttpPatch("me/requests-join/athlete/{athleteUsername}")]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<RequestInfoResponseDto>> UpdateRequestForAddAthleteToCoach(string athleteUsername,
            [FromBody] RequestStatusRequestDto requestStatusDto) =>
             await _requestService.UpdateRequestStatusForAddAthleteToCoachAsync(athleteUsername, User.Identity.Name, requestStatusDto.RequestStatus);
        
        /// <summary>
        /// Respond to a participation in a competition request made by an athlete for the current coach
        /// </summary>
        /// <param name="athleteUsername"></param>
        /// <param name="competitionId"></param>
        /// <param name="requestStatusDto"></param>
        [HttpPatch("me/requests-participate/athlete/{athleteUsername}/competition/{competitionId:guid}")]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<RequestInfoWithCompetitionResponseDto>> UpdateRequestForAddAthleteToCompetition(string athleteUsername,Guid competitionId,
            [FromBody] RequestStatusRequestDto requestStatusDto) =>
             await _requestService.UpdateRequestStatusForAddAthleteToCompetitionAsync(athleteUsername, User.Identity.Name,competitionId, requestStatusDto.RequestStatus);
        
    }
}