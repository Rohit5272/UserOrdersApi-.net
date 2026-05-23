using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserOrderAPI.Data;
using UserOrderAPI.Models;
using UserOrderAPI.DTOs;

namespace UserOrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var order = new Order
            {
                ProductName = dto.ProductName,
                UserId = dto.UserId
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var response = new OrderResponseDto
            {
                Id = order.Id,
                ProductName = order.ProductName,
                UserId = order.UserId
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var orders = await _context.Orders.Include(o => o.User).ToListAsync();

            var response = orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                ProductName = o.ProductName,
                UserId = o.UserId,

                User = o.User == null ? null : new UserResponseDto
                {
                    Id = o.User.Id,
                    Name = o.User.Name
                }
            }).ToList();

            return Ok(response);
        }
    }
}
