using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Auth.Commands;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IUserRepository _users;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<User> _hasher = new();

    public LoginHandler(IUserRepository users, IConfiguration config)
    {
        _users = users;
        _config = config;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var user = await _users.GetByLoginAsync(dto.LoginName);
        if (user == null)
        {
            return new LoginResult { Success = false, Error = "Пользователь не найден" };
        }

        var check = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);
        if (check != PasswordVerificationResult.Success)
        {
            return new LoginResult { Success = false, Error = "Неверный пароль" };
        }

        // JWT
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new LoginResult
        {
            Success = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }
}