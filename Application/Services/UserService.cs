using System.Security.Claims;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IRoleRepository _roleRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PasswordHasher<User> _hasher = new();

    public UserService(IUserRepository repo, IRoleRepository roleRepo, IHttpContextAccessor httpContextAccessor)
    {
        _repo = repo;
        _roleRepo = roleRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<List<User>> GetAllAsync() => _repo.GetAllAsync();

    public async Task<User> CreateAsync(CreateUserDto dto)
    {
        var role = await _roleRepo.GetByNameAsync("User");
        if (role == null)
        {
            throw new Exception("Роль 'User' не найдена");
        }

        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid? adminId = string.IsNullOrEmpty(adminIdString) ? null : Guid.Parse(adminIdString);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            UserName = dto.UserName,
            LoginName = dto.LoginName,
            Password = _hasher.HashPassword(null!, dto.Password),
            RoleId = role.RoleId,
            PhoneNumber = dto.PhoneNumber,
            Info = dto.Info,
            ParentId = adminId,
            Role = role,
        };

        await _repo.AddAsync(user);
        return user;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }

        await _repo.DeleteAsync(user);
        return true;
    }
}