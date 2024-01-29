﻿using dotNet_backend.Models.Participation;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.ParticipationRepository;

public interface IParticipationRepository 
{
    Task CreateAsync(Participation participation);
    Task<bool> SaveAsync();
    Task<IEnumerable<Participation>> FindAllAthletesParticipatingInACompetitionByIdAsync(Guid id);
}