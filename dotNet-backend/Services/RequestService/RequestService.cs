using AutoMapper;
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.RequestRepository;
using dotNet_backend.Services.CoachService;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.RequestService;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly ICoachService _coachService;
    private readonly IMapper _mapper;

    public RequestService(IRequestRepository requestRepository, ICoachRepository coachRepository,
        IAthleteRepository athleteRepository, ICoachService coachService, IMapper mapper)
    {
        _requestRepository = requestRepository;
        _coachRepository = coachRepository;
        _athleteRepository = athleteRepository;
        _coachService = coachService;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetRequestsByUsernameAsync(string username)
    {
        var request = await _requestRepository.FindRequestsAssignedToUsernameAsync(username);
        return _mapper.Map<List<RequestInfoResponseDto>>(request);
    }

    public async Task<ActionResult<RequestInfoResponseDto>> UpdateRequestStatusAsync(string coachUsername,
        string usernameAthlete, string requestStatus)
    {
        var request =
            await _requestRepository.FindRequestAssignedToUsernameAndRequestedByUsername(coachUsername,
                usernameAthlete);
        if (request == null)
            throw new NotFoundException("Request not found for this athlete and coach");
        request.RequestStatus = Enum.Parse<RequestStatus>(requestStatus.ToUpper());
        if (request.RequestStatus == RequestStatus.ACCEPTED)
        {
            await _coachService.AddAthleteToCoach(usernameAthlete, coachUsername);
        }

        _requestRepository.Update(request);
        await _requestRepository.SaveAsync();
        return _mapper.Map<RequestInfoResponseDto>(request);
    }

    public async Task<ActionResult<RequestInfoResponseDto>> CreateRequestAsync(string athleteUsername,
        string coachUsername,
        RequestType requestType)
    {
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        var oldRequest =
            await _requestRepository.FindRequestAssignedToUsernameAndRequestedByUsername(coachUsername,
                athleteUsername);
        if (oldRequest != null)
            throw new BadRequestException("You already have a request for this athlete");

        var request = new RequestInfo
        {
            RequestByUser = athleteUsername,
            AssignedToUser = coachUsername,
            RequestType = requestType,
            RequestStatus = RequestStatus.PENDING,
            RequestDate = DateTime.Now
        };
        await _requestRepository.CreateAsync(request);
        await _requestRepository.SaveAsync();
        return _mapper.Map<RequestInfoResponseDto>(request);
    }
}