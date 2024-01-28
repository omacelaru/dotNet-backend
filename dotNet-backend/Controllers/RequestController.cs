using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Services.RequestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestController(IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService _requestService = requestService;
        
        [HttpPost("join/{coachUsername}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoResponseDto>> JoinCoach(string coachUsername) =>
            await _requestService.CreateRequestAsync(User.Identity.Name, coachUsername,
                RequestType.AddAthleteToCoach);
    }
}