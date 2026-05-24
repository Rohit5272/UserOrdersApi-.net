using UserOrderAPI.DTOs;

namespace UserOrderAPI.Services
{
    public interface IUserService
    {
        Task<object> CreateUser(CreateUserDto dto);

        Task<IEnumerable<UserWithOrdersResponseDto>> GetUsers(
            int page,
            int pageSize,
            string? search,
            string? sortBy);
    }
}
