using dotNet_backend.Models.Request;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.RequestRepository;

public interface IRequestRepository : IGenericRepository<RequestInfo>
{
    Task<IEnumerable<RequestInfo>> FindRequestsAssignedToUsernameAsync(string assignedUsername);
    Task<RequestInfo> FindRequestAssignedToUsernameAndRequestedByUsername
        (string assignedUsername, string requestUsername);
}