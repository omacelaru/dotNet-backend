using dotNet_backend.Models.User;
using dotNet_backend.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_backend.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context){}
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _table.FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _table.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
