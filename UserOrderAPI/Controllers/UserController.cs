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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var result = await _userService.CreateUser(dto);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
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
