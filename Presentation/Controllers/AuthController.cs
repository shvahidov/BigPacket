using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<User> _hasher = new();

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
        if (role == null)
        {
            return BadRequest("Роль User не найдена");
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            UserName = dto.UserName,
            LoginName = dto.LoginName,
            Password = _hasher.HashPassword(null!, dto.Password),
            PhoneNumber = dto.PhoneNumber,
            Info = dto.Info,
            RoleId = role.RoleId,
            Role = role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Пользователь зарегистрирован");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.LoginName == dto.LoginName);

        if (user == null)
        {
            return Unauthorized("Пользователь не найден");
        }

        var result = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);
        if (result != PasswordVerificationResult.Success)
        {
            return Unauthorized("Неверный пароль");
        }

        // создаем JWT токен
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.RoleName) // роль
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}
