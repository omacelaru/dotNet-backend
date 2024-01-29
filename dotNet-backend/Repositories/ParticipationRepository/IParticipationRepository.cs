using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Participation;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.ParticipationRepository;

public interface IParticipationRepository 
{
    Task CreateAsync(Participation participation);
    Task<bool> SaveAsync();
    void Update(Participation participation);
    Task<IEnumerable<Participation>> FindAllAthletesParticipatingInACompetitionByIdAsync(Guid id);
    Task<Participation> FindParticipationByAthleteIdAndCompetitionId(Guid athleteId, Guid competitionId);
    Task<IEnumerable<Participation>> FindAllAthletesForCompetitionByIdAndCoachUsernameAsync(Guid competitionId, string coachUsername);
}