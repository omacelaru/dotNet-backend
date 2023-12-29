using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.RequestRepository;

namespace dotNet_backend.Services.RequestService;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly IAthleteRepository _athleteRepository;

    public RequestService(IRequestRepository requestRepository, ICoachRepository coachRepository, IAthleteRepository athleteRepository)
    {
        _requestRepository = requestRepository;
        _coachRepository = coachRepository;
        _athleteRepository = athleteRepository;
    }

    public async Task JoinCoachAsync(Coach coach, Athlete athlete)
    {
        var request = new RequestInfo
        {
            RequestDate = DateTime.Now,
            RequestByUser = athlete.Username,
            AssignedToUser = coach.Username,
            RequestType = RequestType.AddAthleteToCoach,
            RequestStatus = RequestStatus.Pending
        };
        await _requestRepository.CreateAsync(request);
        await _requestRepository.SaveAsync();
    }
}