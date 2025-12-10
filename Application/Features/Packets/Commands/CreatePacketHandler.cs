using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Commands;

public class CreatePacketHandler : IRequestHandler<CreatePacketCommand, Packet>
{
    private readonly IPacketRepository _repo;

    public CreatePacketHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Packet> Handle(CreatePacketCommand request, CancellationToken ct)
    {
        var packet = new Packet
        {
            PacketId = Guid.NewGuid(),
            PacketTypeId = request.PacketTypeId,
            PacketStatusId = request.PacketStatusId,
            PacketKey = string.IsNullOrWhiteSpace(request.PacketKey)
                ? Guid.NewGuid().ToString("N")
                : request.PacketKey,
            AddDate = DateTime.UtcNow,
            EndDate = request.EndDate,
        };

        await _repo.AddAsync(packet);
        return packet;
    }
}