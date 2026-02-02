using Domain.Entities;

namespace Application.Interfaces;

public interface IPacketRepository
{
    Task<Packet> AddAsync(Packet packet);

    Task<Packet?> GetByIdAsync(Guid id);

    Task<IEnumerable<Packet>> GetAllAsync();

    Task UpdateAsync(Packet packet);

    Task DeleteAsync(Guid id);

    Task<List<Packet>> GetActivePacketsAsync();

    Task SaveChangesAsync();
}