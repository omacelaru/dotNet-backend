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
    
    public async Task<IEnumerable<RequestInfo>> FindRequestsByUsernameAsync(string username)
    {
        return await _table.Where(r => r.AssignedToUser == username && r.RequestStatus == RequestStatus.PENDING).ToListAsync();
    }

    public async Task<RequestInfo> FindRequestByUsernamesAsync(string myUsername, string usernameAthlete)
    {
        return await _table.FirstOrDefaultAsync(r => r.AssignedToUser == myUsername && r.RequestByUser == usernameAthlete && r.RequestStatus == RequestStatus.PENDING);
    }
}