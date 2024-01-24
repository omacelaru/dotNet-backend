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
        
        //respond to request to join in the athlete list of a coach by coach username
        [HttpGet("me/requests")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetRequests()
        {
            
        }
        
        //set request status to accepted/rejected
        [HttpPatch("me/requests/{usernameAthlete}")]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<RequestInfoResponseDto>> PatchRequest(string usernameAthlete, [FromBody] RequestStatusRequestDto requestStatusDto)
        {
        }
        
        
    }
}