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

        
        //get all athletes with pagination and sorting by points
        [HttpGet("athletes")]
        [ValidateModel]
        public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletes([FromQuery] PaginationFilter paginationFilter, [FromQuery] string sortBy) =>
            await _rankService.GetAllAthletesAsync(paginationFilter, sortBy);
        //nu stiu exact cu ce ne ajuta sortBy ca noi oricum sortam dupa puncte; eventual de scos

        //de afisat cluburile si antrenorii cu punctele lor care trebuie calculate cu punctele de la sportivii lor
        [HttpGet("clubs_coaches")]
        [ValidateModel]
        public async Task<ActionResult<IEnumerable<ClubResponseWithPointsDto>>> GetAllClubsAndCoaches([FromQuery] PaginationFilter paginationFilter, [FromQuery] string sortBy) =>
            await _rankService.GetAllClubsAndCoachesAsync(paginationFilter, sortBy);
        
    }
}