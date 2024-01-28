﻿using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Models.Request.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.AthleteService;

public interface IAthleteService
{
    Task<ActionResult<IEnumerable<AthleteResponseDto>>> GetAllAthletesAsync();
    Task<ActionResult<AthleteResponseDto>> GetAthleteByUsernameAsync(string username);
    Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetAthleteRequestsAsync(string username);
    Task<ActionResult<RequestInfoResponseDto>> DeleteAthleteRequestAsync(Guid id, string athleteUsername);
}