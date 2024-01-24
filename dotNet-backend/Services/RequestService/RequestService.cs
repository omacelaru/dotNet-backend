using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.RequestRepository;
using dotNet_backend.Services.CoachService;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.RequestService;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly ICoachService _coachService;

    public RequestService(IRequestRepository requestRepository, ICoachRepository coachRepository, IAthleteRepository athleteRepository, ICoachService coachService)
    {
        _requestRepository = requestRepository;
        _coachRepository = coachRepository;
        _athleteRepository = athleteRepository;
        _coachService = coachService;
    }

    public async Task JoinCoachAsync(Coach coach, Athlete athlete)
    {
        var request = new RequestInfo
        {
            RequestDate = DateTime.Now,
            RequestByUser = athlete.Username,
            AssignedToUser = coach.Username,
            RequestType = RequestType.AddAthleteToCoach,
            RequestStatus = RequestStatus.PENDING
        };
        await _requestRepository.CreateAsync(request);
        await _requestRepository.SaveAsync();
    }
    
    public async Task<IEnumerable<RequestInfo>> GetRequestsByUsernameAsync(string username)
    {
        return await _requestRepository.FindRequestsByUsernameAsync(username);
    }

    public async Task<RequestInfo> UpdateRequestStatusAsync(string coachUsername, string usernameAthlete, string requestStatus)
    {
        var request = await _requestRepository.FindRequestByUsernamesAsync(coachUsername, usernameAthlete);
        if (request == null)
            throw new NotFoundException("Request not found");
        request.RequestStatus = Enum.Parse<RequestStatus>(requestStatus);
        if (request.RequestStatus == RequestStatus.ACCEPTED)
        {
            await _coachService.AddAthleteToCoach(usernameAthlete, coachUsername);
        }
        _requestRepository.Update(request);
        await _requestRepository.SaveAsync();
        return request;
    }
}