using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserOrderAPI.Data;
using UserOrderAPI.DTOs;
using UserOrderAPI.Models;
using UserOrderAPI.Services;

namespace UserOrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var result = await _userService.CreateUser(dto);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(
            int page = 1,
            int pageSize = 5,
            string? search = null,
            string? sortBy = null)
        {
            _logger.LogInformation("GetAllUsers API called");

            var users = await _userService.GetUsers(
                page,pageSize,search,sortBy);

            _logger.LogInformation("Users fetched successfully");

            return Ok(users);
        }

        // checking global exception handling
        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("Something broke");
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, CreateUserDto dto)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null) return NotFound("User not found");

        //    user.Name = dto.Name;

        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null) return NotFound("User Not Found");

        //    _context.Users.Remove(user);

        //    await _context.SaveChangesAsync();

        //    return Ok("Users deleted successfully");
        //}
    }
}
