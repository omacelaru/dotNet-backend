using AutoMapper;
using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Services.ClubService;
using dotNet_backend.Services.CoachService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        public readonly IClubService _clubService;
        public readonly ICoachService _coachService;
        public readonly ILogger<ClubController> _logger;
        public readonly IMapper _mapper;

        public ClubController(IClubService clubService, ICoachService coachService,ILogger<ClubController> logger, IMapper mapper)
        {
            _clubService = clubService;
            _coachService = coachService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Club
        [HttpGet]
        public async Task<IEnumerable<ClubResponseDto>> Get()
        {
            try
            {
                var clubs =  await _clubService.GetAllClubsAsync();
                _logger.LogInformation("Getting all clubs {}", clubs);
                return clubs;
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting all clubs");
                throw new Exception(e.Message);
            }

        }

        // POST: api/Club
        [HttpPost]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ClubResponseDto> Post([FromBody] ClubRequestDto clubRequestDto)
        {
            try
            {
                var club = await _clubService.CreateClubAsync(_mapper.Map<Club>(clubRequestDto));
                var clubWithCoach = await _coachService.AddCoachToClubAsync(User.Identity.Name,club);
                return _mapper.Map<ClubResponseDto>(clubWithCoach);
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating club {}", clubRequestDto);
                throw new Exception(e.Message);
            }
        }

    }
}
