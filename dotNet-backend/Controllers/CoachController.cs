using AutoMapper;
using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Coach;
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
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;
        private readonly IRequestService _requestService;
        private readonly ILogger<CoachController> _logger;

        public CoachController(ICoachService coachService, ILogger<CoachController> logger, IRequestService requestService)
        {
            _coachService = coachService;
            _requestService = requestService;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoachResponseDto>>> GetAllCoaches()
        {
            _logger.LogInformation("Getting all coaches");
            return await _coachService.GetAllCoachesAsync();
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachById(Guid id)
        {
            _logger.LogInformation("Getting coach with id {}", id);
            return await _coachService.GetCoachByIdAsync(id);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachByUserName(string username)
        {
            _logger.LogInformation("Getting coach with username {}", username);
            return await _coachService.GetCoachByUserNameAsync(username);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachByUserName()
        {
            _logger.LogInformation("Getting coach with username {}", User.Identity.Name);
            return await _coachService.GetCoachByUserNameAsync(User.Identity.Name);
        }

        [HttpGet("me/requests")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetRequests()
        {
            string coachUsername = User.Identity.Name;
            _logger.LogInformation("Getting requests for coach {}", coachUsername);
            return await _requestService.GetRequestsByUsernameAsync(coachUsername);
        }
        
        //set request status to accepted/rejected
        [HttpPatch("me/requests/{usernameAthlete}")]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<RequestInfoResponseDto>> PatchRequest(string usernameAthlete, [FromBody] RequestStatusRequestDto requestStatusDto)
        {
            string coachUsername = User.Identity.Name;
            string requestStatus = requestStatusDto.RequestStatus;
            _logger.LogInformation("Setting request status to {} for athlete {} by coach {}", requestStatus, usernameAthlete, coachUsername);
            return await _requestService.UpdateRequestStatusAsync(coachUsername, usernameAthlete, requestStatus);
        }
        
        
    }
}