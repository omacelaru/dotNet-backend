using dotNet_backend.CustomActionFilters;
using dotNet_backend.Helpers.PaginationFilter;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Services.RankService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RankController(IRankService rankService) : ControllerBase
    {
        private readonly IRankService _rankService = rankService;
        
        [HttpGet("athletes")]
        [ValidateModel]
        public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletes([FromQuery] PaginationFilter paginationFilter) =>
            await _rankService.GetAllAthletesAsync(paginationFilter);

        [HttpGet("clubs_coaches")]
        [ValidateModel]
        public async Task<ActionResult<IEnumerable<ClubResponseWithPointsDto>>> GetAllClubsAndCoaches([FromQuery] PaginationFilter paginationFilter) =>
            await _rankService.GetAllClubsAndCoachesAsync(paginationFilter);
        
    }
}