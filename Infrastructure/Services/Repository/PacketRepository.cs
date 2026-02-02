using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Repository;

public class PacketRepository(AppDbContext context) : IPacketRepository
{
    public async Task<Packet> AddAsync(Packet packet)
    {
        context.Packets.Add(packet);
        await context.SaveChangesAsync();
        return packet;
    }

    public async Task<Packet?> GetByIdAsync(Guid id) =>
        await context.Packets
            .FirstOrDefaultAsync(p => p.PacketId == id);

    public async Task<IEnumerable<Packet>> GetAllAsync() =>
        await context.Packets
            .ToListAsync();

    public async Task UpdateAsync(Packet packet)
    {
        context.Packets.Update(packet);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var packet = await context.Packets.FindAsync(id);
        if (packet is not null)
        {
            context.Packets.Remove(packet);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Packet>> GetActivePacketsAsync()
    {
        return await context.Packets
            .Where(p => EF.Property<int>(p, "_statusId") == PacketStatusEnum.Active.Value)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}