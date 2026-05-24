using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<object> CreateUser(CreateUserDto dto)
        {
            //var user = new User
            //{
            //    Name = dto.Name
            //};

            var user = _mapper.Map<User>(dto);

            var createdUser = await _repository.CreateUser(user);

            var response = _mapper.Map<UserResponseDto>(createdUser);

            return response;
        }

        public async Task<IEnumerable<UserWithOrdersResponseDto>> GetUsers(
            int page,
            int pageSize,
            string? search,
            string? sortBy)
        {
            var users = await _repository.GetUsers();

            // filtering
            if(!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            // Sorting
            users = sortBy?.ToLower() switch
            {
                "name" => users.OrderBy(u => u.Name).ToList(),
                "id" => users.OrderBy(u => u.Id).ToList(),
                _ => users
            };

            // Pagination
            users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var response = users.Select(u => new UserWithOrdersResponseDto
            {
                Id = u.Id,
                Name = u.Name,

                Orders = u.Orders?.Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    ProductName = o.ProductName
                }).ToList()
            });

            return response;
        }
    }

}
