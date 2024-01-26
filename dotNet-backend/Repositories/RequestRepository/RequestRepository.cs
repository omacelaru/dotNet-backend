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

    public async Task<RequestInfo> FindRequestAssignedToUsernameAndRequestedByUsername
        (string assignedUsername, string requestUsername)
    {
        return await _table.FirstOrDefaultAsync(r => r.AssignedToUser == assignedUsername && r.RequestByUser == requestUsername && r.RequestStatus == RequestStatus.PENDING);
    }
}