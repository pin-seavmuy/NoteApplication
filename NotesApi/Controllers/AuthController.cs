using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using NotesApi.Repositories;
using NotesApi.Services;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepo, JwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { message = "Username and password are required." });

            var existing = await _userRepo.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return BadRequest(new { message = "Username already exists." });

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = passwordHash
            };

            var created = await _userRepo.CreateAsync(user);
            var token = _jwtService.GenerateToken(created.Id, created.Username);

            return Ok(new
            {
                userId = created.Id,
                username = created.Username,
                token = token
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid credentials." });

            var token = _jwtService.GenerateToken(user.Id, user.Username);

            return Ok(new
            {
                userId = user.Id,
                username = user.Username,
                token = token
            });
        }
    }
}
