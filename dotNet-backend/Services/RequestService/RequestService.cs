using AutoMapper;
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.CompetitionRepository;
using dotNet_backend.Repositories.RequestRepository;
using dotNet_backend.Services.CoachService;
using dotNet_backend.Services.ParticipationService;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.RequestService;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly ICoachService _coachService;
    private readonly IParticipationService _participationService;
    private readonly ILogger<RequestService> _logger;
    private readonly IMapper _mapper;

    public RequestService(IRequestRepository requestRepository, ICoachRepository coachRepository,
        IAthleteRepository athleteRepository, ICompetitionRepository competitionRepository, ICoachService coachService,
        IParticipationService participationService, ILogger<RequestService> logger,
        IMapper mapper)
    {
        _requestRepository = requestRepository;
        _coachRepository = coachRepository;
        _athleteRepository = athleteRepository;
        _competitionRepository = competitionRepository;
        _coachService = coachService;
        _participationService = participationService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<RequestInfoResponseDto>> UpdateRequestStatusForAddAthleteToCoachAsync(
        string athleteUsername, string coachUsername, string requestStatus)
    {
        _logger.LogInformation("Updating request-join status for athlete {} and coach {} to {}", athleteUsername,
            coachUsername, requestStatus);
        var status = Enum.Parse<RequestStatus>(requestStatus.ToUpper());
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

    public async Task<ActionResult<RequestInfoResponseDto>> CreateRequestToJoinInCoachListAsync(
        string athleteUsername,
        string coachUsername)
    {
        _logger.LogInformation("Athlete {} is requesting to join coach {}", athleteUsername, coachUsername);
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        if (coach == null)
        {
            _logger.LogError("Coach {} not found", coachUsername);
            throw new NotFoundException("Coach not found");
        }

        await ValidateCreateRequestToJoinInCoachListAsync(athlete, coach);
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

    private async Task ValidateCreateRequestToJoinInCoachListAsync(Athlete athlete, Coach coach)
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

    public async Task<ActionResult<RequestInfoWithCompetitionResponseDto>> CreateRequestToParticipateInCompetitionAsync(
        string athleteUsername, Guid competitionId)
    {
        _logger.LogInformation("Athlete {} is requesting to participate in competition {}", athleteUsername,
            competitionId);
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        if (athlete == null)
        {
            _logger.LogError("Athlete {} not found", athleteUsername);
            throw new NotFoundException("Athlete not found");
        }

        if (athlete.Coach == null)
        {
            _logger.LogError("Athlete {} does not have a coach", athleteUsername);
            throw new BadRequestException("You don't have a coach");
        }

        var competition = await _competitionRepository.FindCompetitionByIdAsync(competitionId);
        if (competition == null)
        {
            _logger.LogError("Competition {} not found", competitionId);
            throw new NotFoundException("Competition not found");
        }

        var oldRequest =
            await _requestRepository.FindRequestToAddAthleteToCompetitionByUsernameAsync(athlete.Username,
                competitionId);
        if (oldRequest != null)
        {
            _logger.LogError("Athlete {} already has a request for this competition {}", athlete.Username,
                competitionId);
            throw new BadRequestException("You already have a request for this competition");
        }

        var request = new RequestInfo
        {
            RequestedByUser = athleteUsername,
            AssignedToUser = athlete.Coach.Username,
            CompetitionId = competition.Id,
            CompetitionName = competition.Name,
            RequestType = RequestType.AddAthleteToCompetition,
            RequestStatus = RequestStatus.PENDING,
            RequestDate = DateTime.Now
        };
        await _requestRepository.CreateAsync(request);
        await _requestRepository.SaveAsync();
        return _mapper.Map<RequestInfoWithCompetitionResponseDto>(request);
    }

    public async Task<ActionResult<RequestInfoWithCompetitionResponseDto>>
        UpdateRequestStatusForAddAthleteToCompetitionAsync(
            string athleteUsername, string coachUsername, Guid competitionId, string requestStatus)
    {
        _logger.LogInformation("Updating request-participate status for athlete {} and coach {} to {}", athleteUsername,
            coachUsername, requestStatus);
        var status = Enum.Parse<RequestStatus>(requestStatus.ToUpper());
        var request = await _requestRepository.FindRequestToAddAthleteToCompetitionByUsernameAsync(athleteUsername,
            competitionId);
        if (request == null)
        {
            _logger.LogError("Request not found for athlete {} and competition {}", athleteUsername, competitionId);
            throw new NotFoundException("Request not found");
        }

        request.RequestStatus = status;
        _requestRepository.Update(request);
        await _requestRepository.SaveAsync();
        if (status == RequestStatus.ACCEPTED)
            await _participationService.AddAthleteToCompetitionAsync(competitionId,athleteUsername, coachUsername);
        return _mapper.Map<RequestInfoWithCompetitionResponseDto>(request);
    }
}