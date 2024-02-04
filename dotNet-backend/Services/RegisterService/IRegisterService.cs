using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.User.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.RegisterService
{
    public interface IRegisterService
    {
        Task<ActionResult<AthleteCoachNameResponseDto>> RegisterAthleteAsync(RegisterDto athleteRegisterDto);

        Task<ActionResult<CoachResponseDto>> RegisterCoachAsync(RegisterDto coachRegisterDto);

        Task<IActionResult> RegisterAdminAsync(RegisterDto adminRegisterDto);
    }
}
