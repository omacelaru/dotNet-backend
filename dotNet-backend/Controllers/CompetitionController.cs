using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Competition.DTO;
using dotNet_backend.Services.CompetitionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompetitionController(ICompetitionService competitionService) : ControllerBase
    {
        private readonly ICompetitionService _competitionService = competitionService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitionResponseDto>>> GetAllCompetitions() =>
            await _competitionService.GetAllCompetitions();
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
    public async Task<ActionResult<CompetitionResponseDto>> CreateCompetition([FromBody] CompetitionRequestDto competitionRequestDto) => 
            await _competitionService.CreateCompetitionAsync(competitionRequestDto);
        
    }
}
