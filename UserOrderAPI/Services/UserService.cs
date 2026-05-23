using Microsoft.EntityFrameworkCore;
using UserOrderAPI.Data;
using UserOrderAPI.DTOs;
using UserOrderAPI.Models;
using UserOrderAPI.Repositories;

namespace UserOrderAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> CreateUser(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name
            };

            var createdUser = await _repository.CreateUser(user);

            return new {
                Id = createdUser.Id,
                Name = createdUser.Name
            };
        }

        public async Task<IEnumerable<UserWithOrdersResponseDto>> GetUsers()
        {
            var users = await _repository.GetUsers();

            var response = users.Select(u => new UserWithOrdersResponseDto
            {
                Id = u.Id,
                Name = u.Name,

                Orders = u.Orders.Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    ProductName = o.ProductName
                }).ToList()
            });

            return response;
        }
    }
}
