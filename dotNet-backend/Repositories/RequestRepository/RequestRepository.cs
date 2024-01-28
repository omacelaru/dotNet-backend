using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.RequestRepository;

public class RequestRepository : GenericRepository<RequestInfo>, IRequestRepository
{
    public RequestRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RequestInfo>> FindRequestsAssignedToUsernameAsync(string assignedUsername)
    {
        return await _table.Where(r => r.AssignedToUser == assignedUsername).AsNoTracking().ToListAsync();
    }

    public async Task<RequestInfo> FindRequestToAddAthleteToCoachByUsernameAsync
        (string athleteUsername, string coachUsername)
    {
        return await _table.FirstOrDefaultAsync(r =>
            r.AssignedToUser == coachUsername && r.RequestedByUser == athleteUsername &&
            r.RequestStatus == RequestStatus.PENDING && r.RequestType == RequestType.AddAthleteToCoach);
    }

    public async Task<RequestInfo> FindRequestToAddAthleteToCoachByUsernameAsync
        (string athleteUsername)
    {
        return await _table.FirstOrDefaultAsync(r =>
            r.RequestedByUser == athleteUsername &&
            r.RequestStatus == RequestStatus.PENDING && r.RequestType == RequestType.AddAthleteToCoach);
    }
}