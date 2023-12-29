using AutoMapper;
using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Services.CoachService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        public readonly ICoachService _coachService;
        public readonly ILogger<CoachController> _logger;
        public readonly IMapper _mapper;

        public CoachController(ICoachService coachService, ILogger<CoachController> logger, IMapper mapper)
        {
            _coachService = coachService;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IEnumerable<CoachResponseDto>> GetAllCoaches()
        {
            try
            {
                var coaches = await _coachService.GetAllCoachesAsync();
                _logger.LogInformation("Getting all coaches {}", coaches);
                return _mapper.Map<IEnumerable<CoachResponseDto>>(coaches);
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting all coaches");
                throw new Exception(e.Message);
            }

        }
        
        [HttpGet("{id:guid}")]
        public async Task<CoachResponseDto> GetCoachById(Guid id)
        {
            try
            {
                var coach = await _coachService.GetCoachByIdAsync(id);
                _logger.LogInformation("Getting coach {}", coach);
                return _mapper.Map<CoachResponseDto>(coach);
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting coach with id {}", id);
                throw new Exception(e.Message);
            }
        }


        // // PUT: api/Coach/5
        // [HttpPut("{id}")]
        // [Authorize(Roles = "Coach")]
        // [ValidateModel]
        // public async Task<CoachResponseDto> Put(string id, [FromBody] CoachRequestDto coachRequestDto)
        // {
        //     try
        //     {
        //         var coach = await _coachService.UpdateCoachAsync(id, _mapper.Map<Coach>(coachRequestDto));
        //         return _mapper.Map<CoachResponseDto>(coach);
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.LogError("Error updating coach with id {}", id);
        //         throw new Exception(e.Message);
        //     }
        // }
    }
}