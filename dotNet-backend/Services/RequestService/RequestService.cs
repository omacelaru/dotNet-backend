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
    private readonly ILogger<RequestService> _logger;
    private readonly IMapper _mapper;

    public RequestService(IRequestRepository requestRepository, ICoachRepository coachRepository,
        IAthleteRepository athleteRepository, ICoachService coachService, ILogger<RequestService> logger,
        IMapper mapper)
    {
        _requestRepository = requestRepository;
        _coachRepository = coachRepository;
        _athleteRepository = athleteRepository;
        _coachService = coachService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetRequestsByUsernameAsync(string username)
    {
        var request = await _requestRepository.FindRequestsAssignedToUsernameAsync(username);
        return _mapper.Map<List<RequestInfoResponseDto>>(request);
    }

    public async Task<ActionResult<RequestInfoResponseDto>> UpdateRequestStatusAsync(string requestedByUsername,
        string assignedToUser, string requestStatus, RequestType requestType)
    {
        var status = Enum.Parse<RequestStatus>(requestStatus.ToUpper());
        return requestType switch
        {
            RequestType.AddAthleteToCoach => await UpdateRequestStatusToAddAthleteToCoachAsync(requestedByUsername,
                assignedToUser, status),
            _ => throw new BadRequestException("Request type not found")
        };
    }

    private async Task<ActionResult<RequestInfoResponseDto>> UpdateRequestStatusToAddAthleteToCoachAsync(
        string athleteUsername, string coachUsername, RequestStatus status)
    {
        var request = await _requestRepository.FindRequestToAddAthleteToCoachByUsernameAsync(athleteUsername,
            coachUsername);
        if (request == null)
        {
            _logger.LogError("Request not found for athlete {} and coach {}", athleteUsername, coachUsername);
            throw new NotFoundException("Request not found");
        }

        request.RequestStatus = status;
        _requestRepository.Update(request);
        await _requestRepository.SaveAsync();
        if (status == RequestStatus.ACCEPTED)
            await _coachService.AddAthleteToCoachAsync(athleteUsername, coachUsername);
        return _mapper.Map<RequestInfoResponseDto>(request);
    }

    public async Task<ActionResult<RequestInfoResponseDto>> CreateRequestAsync(string requestedByUsername,
        string assignedToUser,
        RequestType requestType)
    {
        return requestType switch
        {
            RequestType.AddAthleteToCoach => await CreateRequestToAddAthleteToCoachAsync(requestedByUsername,
                assignedToUser),
            _ => throw new BadRequestException("Request type not found")
        };
    }

    private async Task<ActionResult<RequestInfoResponseDto>> CreateRequestToAddAthleteToCoachAsync(
        string athleteUsername,
        string coachUsername)
    {
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        if (coach == null)
        {
            _logger.LogError("Coach {} not found", coach.Username);
            throw new NotFoundException("Coach not found");
        }

        await ValidateCreateRequest(athlete, coach);
        var request = new RequestInfo
        {
            RequestedByUser = athleteUsername,
            AssignedToUser = coachUsername,
            RequestType = RequestType.AddAthleteToCoach,
            RequestStatus = RequestStatus.PENDING,
            RequestDate = DateTime.Now
        };
        await _requestRepository.CreateAsync(request);
        await _requestRepository.SaveAsync();
        return _mapper.Map<RequestInfoResponseDto>(request);
    }

    private async Task ValidateCreateRequest(Athlete athlete, Coach coach)
    {
        if (athlete.Coach != null)
        {
            _logger.LogError("Athlete {} already has a coach", athlete.Username);
            throw new BadRequestException("You already have a coach");
        }
        var oldRequest =
            await _requestRepository.FindRequestToAddAthleteToCoachByUsernameAsync(athlete.Username, coach.Username);
        if (oldRequest != null)
        {
            _logger.LogError("Athlete {} already has a request for this coach {}", athlete.Username, coach.Username);
            throw new BadRequestException("You already have a request for this coach");
        }
        var requestForAnotherCoach =
            await _requestRepository.FindRequestToAddAthleteToCoachByUsernameAsync(athlete.Username);
        if (requestForAnotherCoach != null)
        {
            _logger.LogError("Athlete {} already has a request for another coach", athlete.Username);
            throw new BadRequestException("You already have a request for another coach");
        }

        
    }
}