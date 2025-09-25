using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PacketService
{
    private readonly IPacketRepository _repo;

    public PacketService(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Packet> CreatePacketAsync(CreatePacketDto dto)
    {
        var packet = new Packet
        {
            PacketId = Guid.NewGuid(),
            PacketTypeId = dto.PacketTypeId,
            PacketStatusId = dto.PacketStatusId,
            PacketKey = string.IsNullOrWhiteSpace(dto.PacketKey)
                ? Guid.NewGuid().ToString("N")
                : dto.PacketKey,
            AddDate = DateTime.UtcNow,
            EndDate = dto.EndDate,
            PacketType = null,
            PacketStatus = null,
        };

        await _repo.AddAsync(packet);
        return packet;
    }

    public async Task<Packet?> UpdatePacketAsync(Guid id, CreatePacketDto dto)
    {
        var packet = await _repo.GetByIdAsync(id);
        if (packet == null)
        {
            return null;
        }

        packet.PacketTypeId = dto.PacketTypeId;
        packet.PacketStatusId = dto.PacketStatusId;
        packet.PacketKey = dto.PacketKey;
        packet.EndDate = dto.EndDate;

        await _repo.UpdateAsync(packet);
        return packet;
    }

    public Task<IEnumerable<Packet>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Packet?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
}