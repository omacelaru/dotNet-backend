using AutoMapper;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Services.ClubService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        public readonly IClubService _clubService;
        public readonly ILogger<ClubController> _logger;
        public readonly IMapper _mapper;

        public ClubController(IClubService clubService, ILogger<ClubController> logger, IMapper mapper)
        {
            _clubService = clubService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Club
        [HttpGet]
        public async Task<IEnumerable<Club>> Get()
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
        public async Task<Club> Post([FromBody] ClubRequestDto clubRequestDto)
        {
            try
            {
                var club = _mapper.Map<Club>(clubRequestDto);
                return await _clubService.CreateClubAsync(club);

            }
            catch (Exception e)
            {
                _logger.LogError("Error creating club {}", clubRequestDto);
                throw new Exception(e.Message);
            }
        }

    }
}
