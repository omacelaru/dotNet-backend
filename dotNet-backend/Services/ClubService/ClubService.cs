﻿using AutoMapper;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.ClubService
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly ILogger<ClubService> _logger;
        private readonly IMapper _mapper;

        public ClubService(IClubRepository clubRepository, ICoachRepository coachRepository, IMapper mapper,
            ILogger<ClubService> logger)
        {
            _clubRepository = clubRepository;
            _coachRepository = coachRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActionResult<ClubResponseDto>> CreateClubAsync(ClubRequestDto clubRequestDto, string coachUsername)
        {
            _logger.LogInformation("Creating club {} from coach with username: {} ", clubRequestDto, coachUsername);
            var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
            if (coach.Club != null)
            {
                _logger.LogError("Coach with username {} already has a club", coachUsername);
                throw new BadRequestException(
                    "You already has a club. You must delete the current club to create a new one");
            }
            var club = _mapper.Map<Club>(clubRequestDto);
            club.Coach = coach;
            await _clubRepository.CreateAsync(club);
            await _clubRepository.SaveAsync();
            return _mapper.Map<ClubResponseDto>(club);
        }

        public async Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubsAsync()
        {
            _logger.LogInformation("Getting all clubs");
            var clubs = await _clubRepository.FindAllClubsAsync();
            return _mapper.Map<List<ClubResponseDto>>(clubs);
        }
        
        public async Task<ActionResult<ClubResponseDto>> DeleteClubAsync(string coachUsername)
        {
            _logger.LogInformation("Deleting club from coach with username: {} ", coachUsername);
            var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
            if (coach.Club == null)
            {
                _logger.LogError("Coach with username {} doesn't have a club", coachUsername);
                throw new BadRequestException("You don't have a club");
            }
            var club = await _clubRepository.FindClubByIdAsync(coach.Club.Id);
            _clubRepository.Delete(club);
            await _clubRepository.SaveAsync();
            return _mapper.Map<ClubResponseDto>(club);
        }
        
        public async Task<ActionResult<ClubResponseDto>> UpdateClubAsync(ClubRequestDto clubRequestDto, string coachUsername)
        {
            _logger.LogInformation("Updating club {} from coach with username: {} ", clubRequestDto, coachUsername);
            var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
            if (coach.Club == null)
            {
                _logger.LogError("Coach with username {} doesn't have a club", coachUsername);
                throw new BadRequestException("You don't have a club");
            }
            var club = await _clubRepository.FindClubByIdAsync(coach.Club.Id);
            _mapper.Map(clubRequestDto, club);
            _clubRepository.Update(club);
            await _clubRepository.SaveAsync();
            return _mapper.Map<ClubResponseDto>(club);
        }
    }
}