using dotNet_backend.Helpers.PaginationFilter;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Services.RankService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RankController(IRankService rankService) : ControllerBase
    {
        private readonly IRankService _rankService = rankService;
        
        //get all athletes with pagination and sorting by points
        [HttpGet("athletes")]
        public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletes([FromQuery] PaginationFilter paginationFilter, [FromQuery] string sortBy) =>
            await _rankService.GetAllAthletesAsync(paginationFilter, sortBy);
        
    }
}