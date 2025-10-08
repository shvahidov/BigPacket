using Domain.Entities;

namespace Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName);
}