using dotNet_backend.Models.Request;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.RequestRepository;

public interface IRequestRepository : IGenericRepository<RequestInfo>
{
    Task<IEnumerable<RequestInfo>> FindAllRequestsAssignedToUsernameAsync(string assignedUsername);
    Task<IEnumerable<RequestInfo>> FindAllRequestsRequestedByUsernameAsync(string requestedByUsername);

    Task<RequestInfo> FindRequestToAddAthleteToCoachByUsernameAsync
        (string athleteUsernam);
    Task<RequestInfo> FindRequestToAddAthleteToCoachByUsernameAsync
        (string athleteUsername, string coachUsername);

    Task<RequestInfo> FindRequestToAddAthleteToCoachByUsernameAndRequestIdAsync(string athleteUsername, Guid id);
    Task<RequestInfo> FindRequestToAddAthleteToCompetitionByUsernameAsync(string athleteUsername, Guid competitionId);
}