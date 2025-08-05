using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Data;
using ToDoAppApi.DTOs;
using ToDoAppApi.Models;
using ToDoAppApi.Services;
using System.Security.Cryptography;
using System.Text;

namespace ToDoAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username already exists.");

            using var hmac = new HMACSHA256();
            var user = new User
            {
                Username = dto.Username,
                PasswordSalt = hmac.Key, // Save the key
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)))
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            using var hmac = new HMACSHA256(user.PasswordSalt); // Use the saved key!
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));

            if (computedHash != user.PasswordHash)
                return Unauthorized("Invalid username or password.");

            string token = _tokenService.CreateToken(user);
            return Ok(token);
        }

    }
}
