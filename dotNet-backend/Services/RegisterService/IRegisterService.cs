using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;

namespace dotNet_backend.Services.RegisterService
{
    public interface IRegisterService
    {
        Task<Athlete> RegisterAthleteAsync(AthleteRegisterDto athleteRegisterDto);

        Task<Coach> RegisterCoachAsync(CoachRegisterDto coachRegisterDto);

    }
}
