using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;

namespace dotNet_backend.Services.RequestService;

public interface IRequestService
{
    Task JoinCoachAsync(Coach coach, Athlete athlete);
}