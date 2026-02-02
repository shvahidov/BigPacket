using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Repository;

public sealed class PacketTypeRepository(AppDbContext context) : IPacketTypeRepository
{
    public async Task<PacketType?> GetByIdAsync(Guid id)
    {
        return await context.PacketTypes
            .FirstOrDefaultAsync(x => x.PacketTypeId == id);
    }
}