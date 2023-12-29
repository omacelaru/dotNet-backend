using AutoMapper;
using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Services.ClubService;
using dotNet_backend.Services.CoachService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
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
        
        [HttpPost]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ClubResponseDto> Post([FromBody] ClubRequestDto clubRequestDto)
        {
            //create club and add coach to coach list in club
            try
            {
                var club = _mapper.Map<Club>(clubRequestDto);
                var coach = await _coachService.GetCoachByUserNameAsync(User.Identity.Name);
                club.Coaches = new List<Coach>() {coach};
                await _clubService.CreateClubAsync(club);
                _logger.LogInformation("Creating club {}", club);
                return _mapper.Map<ClubResponseDto>(club);
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating club {}", clubRequestDto);
                throw new Exception(e.Message);
            }
        }

    }
}
