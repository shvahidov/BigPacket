using Domain.Entities;

namespace Application.Interfaces;

public interface IPacketTypeRepository
{
    Task<PacketType?> GetByIdAsync(Guid id);
}