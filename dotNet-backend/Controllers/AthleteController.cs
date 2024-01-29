﻿using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Services.AthleteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AthleteController(IAthleteService athleteService) : ControllerBase
    {
        private readonly IAthleteService _athleteService = athleteService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletes() => 
            await _athleteService.GetAllAthletesAsync();
        
        [HttpGet("me")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<AthleteCoachNameResponseDto>> GetAthlete() => 
            await _athleteService.GetAthleteByUsernameAsync(User.Identity.Name);
        
        [HttpGet("me/requests")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetAthleteRequests() => 
            await _athleteService.GetAthleteRequestsAsync(User.Identity.Name);
        
        [HttpDelete("me/requests/{Id:Guid}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoResponseDto>> DeleteAthleteRequest(Guid Id) => 
            await _athleteService.DeleteAthleteRequestAsync(Id, User.Identity.Name);
    }
}
