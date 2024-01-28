using dotNet_backend.Models.Request;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.RequestRepository;

public interface IRequestRepository : IGenericRepository<RequestInfo>
{
    Task<IEnumerable<RequestInfo>> FindRequestsAssignedToUsernameAsync(string assignedUsername);

    Task<RequestInfo> FindRequestToAddAthleteToCoachByUsernameAsync
        (string athleteUsernam);
    Task<RequestInfo> FindRequestToAddAthleteToCoachByUsernameAsync
        (string athleteUsername, string coachUsername);
}