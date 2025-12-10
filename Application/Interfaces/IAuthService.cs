using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<User> RegisterAsync(RegisterDto dto);

    Task<string> LoginAsync(LoginDto dto);
}