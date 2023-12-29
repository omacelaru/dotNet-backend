using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;

namespace dotNet_backend.Services.RegisterService
{
    public interface IRegisterService
    {
        Task<AthleteResponseDto> RegisterAthleteAsync(AthleteRegisterDto athleteRegisterDto);

        Task<CoachResponseDto> RegisterCoachAsync(CoachRegisterDto coachRegisterDto);

    }
}
