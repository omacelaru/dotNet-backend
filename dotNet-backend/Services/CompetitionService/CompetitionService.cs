﻿using AutoMapper;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Competition;
using dotNet_backend.Models.Competition.DTO;
using dotNet_backend.Repositories.CompetitionRepository;
using dotNet_backend.Repositories.ParticipationRepository;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.CompetitionService;

public class CompetitionService: ICompetitionService
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IParticipationRepository _participationRepository;
    private readonly ILogger<CompetitionService> _logger;
    private readonly IMapper _mapper;

    public CompetitionService(ICompetitionRepository competitionRepository,IParticipationRepository participationRepository,ILogger<CompetitionService> logger,IMapper mapper)
    {
        _competitionRepository = competitionRepository;
        _participationRepository = participationRepository;
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
    
    public async Task<ActionResult<CompetitionResponseDto>> DeleteCompetitionAsync(Guid id)
    {
        _logger.LogInformation("Deleting competition with id {}", id);
        var competition = await _competitionRepository.FindCompetitionByIdAsync(id);
        if (competition == null)
        {
            _logger.LogError("Competition with id {} not found", id);
            throw new NotFoundException("Competition not found");
        }
        _competitionRepository.Delete(competition);
        await _competitionRepository.SaveAsync();
        return _mapper.Map<CompetitionResponseDto>(competition);
    }
    
    public async Task<ActionResult<CompetitionResponseDto>> GetCompetitionByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting competition with id {}", id);
        var competition = await _competitionRepository.FindCompetitionByIdAsync(id);
        if (competition == null)
        {
            _logger.LogError("Competition with id {} not found", id);
            throw new NotFoundException("Competition not found");
        }
        return _mapper.Map<CompetitionResponseDto>(competition);
    }
    
    public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetCompetitionAthletesAsync(Guid id)
    {
        _logger.LogInformation("Getting competition {} athletes", id);
        var competition = await _competitionRepository.FindCompetitionByIdAsync(id);
        if (competition == null)
        {
            _logger.LogError("Competition with id {} not found", id);
            throw new NotFoundException("Competition not found");
        }
        var participations = await _participationRepository.FindAllAthletesParticipatingInACompetitionByIdAsync(id);
        if (participations == null)
        {
            _logger.LogError("No athletes found for competition {}", id);
            throw new NotFoundException("No athletes found for competition");
        }
        return _mapper.Map<List<AthleteCoachNameResponseDto>>(participations);
    }
}