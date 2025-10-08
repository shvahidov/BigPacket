using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IRoleRepository _roleRepo;

    public UserService(IUserRepository repo, IRoleRepository roleRepo)
    {
        _repo = repo;
        _roleRepo = roleRepo;
    }

    public Task<List<User>> GetAllAsync() => _repo.GetAllAsync();

    public async Task<User> CreateAsync(CreateUserDto dto)
    {
        var role = await _roleRepo.GetByNameAsync("User");
        if (role == null)
        {
            throw new Exception("Роль 'User' не найдена");
        }

        var user = new User
        {
            UserId = default,
            UserName = dto.UserName,
            LoginName = dto.LoginName,
            Password = dto.Password,
            RoleId = role.RoleId,
            PhoneNumber = dto.PhoneNumber,
            Info = dto.Info,
            ParentId = null,
            Role = null,
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