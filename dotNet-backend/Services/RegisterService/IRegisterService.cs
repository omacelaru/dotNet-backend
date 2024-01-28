using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.RegisterService
{
    public interface IRegisterService
    {
        Task<ActionResult<AthleteResponseDto>> RegisterAthleteAsync(AthleteRegisterDto athleteRegisterDto);

        Task<ActionResult<CoachResponseDto>> RegisterCoachAsync(CoachRegisterDto coachRegisterDto);

    }
}
