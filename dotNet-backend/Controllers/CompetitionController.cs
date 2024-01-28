using dotNet_backend.Services.CompetitionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompetitionController(ICompetitionService competitionService) : ControllerBase
    {
        private readonly ICompetitionService _competitionService = competitionService;
        
    }
}
