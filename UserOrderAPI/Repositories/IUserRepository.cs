using UserOrderAPI.Models;

namespace UserOrderAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);

        Task<List<User>> GetUsers();
    }
}
