using AutoMapper;
using dotNet_backend.Models.Competition;
using dotNet_backend.Models.Competition.DTO;
using dotNet_backend.Repositories.CompetitionRepository;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.CompetitionService;

public class CompetitionService: ICompetitionService
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly ILogger<CompetitionService> _logger;
    private readonly IMapper _mapper;

    public CompetitionService(ICompetitionRepository competitionRepository,ILogger<CompetitionService> logger,IMapper mapper)
    {
        _competitionRepository = competitionRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<CompetitionResponseDto>>> GetAllCompetitions()
    {
        _logger.LogInformation("Getting all competitions");
        var competitions = await _competitionRepository.GetAllCompetitions();
        return _mapper.Map<List<CompetitionResponseDto>>(competitions);
    }
    
    public async Task<ActionResult<CompetitionResponseDto>> CreateCompetitionAsync(CompetitionRequestDto competitionRequestDto)
    {
        _logger.LogInformation("Creating competition {}", competitionRequestDto);
        var competition = _mapper.Map<Competition>(competitionRequestDto);
        var createdCompetition = await _competitionRepository.CreateCompetitionAsync(competition);
        return _mapper.Map<CompetitionResponseDto>(createdCompetition);
    }
}