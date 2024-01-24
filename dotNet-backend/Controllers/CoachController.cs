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
        public readonly ICoachService _coachService;
        public readonly IRequestService _requestService;
        public readonly ILogger<CoachController> _logger;
        public readonly IMapper _mapper;

        public CoachController(ICoachService coachService, ILogger<CoachController> logger, IMapper mapper, IRequestService requestService)
        {
            _coachService = coachService;
            _requestService = requestService;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoachResponseDto>>> GetAllCoaches()
        {
            _logger.LogInformation("Getting all coaches");
            var coaches = await _coachService.GetAllCoachesAsync();
            var coachResponseDtos = _mapper.Map<IEnumerable<CoachResponseDto>>(coaches);
            return Ok(coachResponseDtos);

        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachById(Guid id)
        {
            _logger.LogInformation("Getting coach with id {}", id);
            var coach = await _coachService.GetCoachByIdAsync(id);
            var coachResponseDto = _mapper.Map<CoachResponseDto>(coach);
            return Ok(coachResponseDto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachByUserName(string username)
        {
            _logger.LogInformation("Getting coach with username {}", username);
            var coach = await _coachService.GetCoachByUserNameAsync(username);
            var coachResponseDto = _mapper.Map<CoachResponseDto>(coach);
            return Ok(coachResponseDto);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<CoachResponseDto>> GetCoachByUserName()
        {
            _logger.LogInformation("Getting coach with username {}", User.Identity.Name);
            var coach = await _coachService.GetCoachByUserNameAsync(User.Identity.Name);
            var coachResponseDto = _mapper.Map<CoachResponseDto>(coach);
            return Ok(coachResponseDto);
        }

        [HttpGet("me/requests")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetRequests()
        {
            string coachUsername = User.Identity.Name;
            _logger.LogInformation("Getting requests for coach {}", coachUsername);
            var requests = await _requestService.GetRequestsByUsernameAsync(coachUsername);
            var requestResponseDtos = _mapper.Map<IEnumerable<RequestInfoResponseDto>>(requests);
            return Ok(requestResponseDtos);
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
            var request = await _requestService.UpdateRequestStatusAsync(coachUsername, usernameAthlete, requestStatus);
            var requestResponseDto = _mapper.Map<RequestInfoResponseDto>(request);
            return Ok(requestResponseDto);
        }
        
        
    }
}