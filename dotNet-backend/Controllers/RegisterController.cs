﻿using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Services.RegisterService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILogger<RegisterController> _logger;
        public RegisterController(IRegisterService registerService, ILogger<RegisterController> logger) 
        {
            _registerService = registerService;
            _logger = logger;
        }
        [HttpPost("athlete")]
        public async Task<IActionResult> RegisterAthlete([FromBody] AthleteRegisterDto athleteRegisterDto)
        {
            try
            {
                return Ok(await _registerService.RegisterAthleteAsync(athleteRegisterDto));

            }
            catch (AuthorizationException)
            {
                return Unauthorized();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("coach")]
        public async Task<IActionResult> RegisterCoach([FromBody] CoachRegisterDto coachRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering coach {}", coachRegisterDto);
                return Ok(await _registerService.RegisterCoachAsync(coachRegisterDto));

            }
            catch (AuthorizationException)
            {
                _logger.LogError("Unauthorized registering coach {}", coachRegisterDto);
                return Unauthorized();
            }
            catch (ArgumentException exception)
            {
                _logger.LogError("Email already exists registering coach {}", coachRegisterDto);
                return BadRequest(error: exception.Message);
            }
            catch
            {
                _logger.LogError("Error registering coach {}", coachRegisterDto);
                return StatusCode(500);
            }
        }   

    }
}