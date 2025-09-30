using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public Task<List<User>> GetAllAsync() => _repo.GetAllAsync();

    public async Task<User> CreateAsync(User user)
    {
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