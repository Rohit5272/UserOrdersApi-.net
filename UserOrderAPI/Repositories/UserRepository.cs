using Microsoft.EntityFrameworkCore;
using UserOrderAPI.Data;
using UserOrderAPI.Models; // Add this using directive to resolve 'User'

namespace UserOrderAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Include(u => u.Orders).ToListAsync();

        }
    }
}
