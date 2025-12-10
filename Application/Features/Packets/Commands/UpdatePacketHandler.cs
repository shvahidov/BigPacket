using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Commands;

public class UpdatePacketHandler : IRequestHandler<UpdatePacketCommand, Packet?>
{
    private readonly IPacketRepository _repo;

    public UpdatePacketHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Packet?> Handle(UpdatePacketCommand request, CancellationToken ct)
    {
        var packet = await _repo.GetByIdAsync(request.Id);
        if (packet == null)
        {
            return null;
        }

        packet.PacketTypeId = request.PacketTypeId;
        packet.PacketStatusId = request.PacketStatusId;
        packet.PacketKey = request.PacketKey;
        packet.EndDate = request.EndDate;

        await _repo.UpdateAsync(packet);
        return packet;
    }
}