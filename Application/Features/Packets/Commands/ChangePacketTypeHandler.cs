using Application.Interfaces;
using MediatR;

namespace Application.Features.Packets.Commands;

public class ChangePacketTypeHandler : IRequestHandler<ChangePacketTypeCommand>
{
    private readonly IPacketRepository _packetRepo;
    private readonly IPacketTypeRepository _packetTypeRepo;

    public ChangePacketTypeHandler(
        IPacketRepository packetRepo,
        IPacketTypeRepository packetTypeRepo)
    {
        _packetRepo = packetRepo;
        _packetTypeRepo = packetTypeRepo;
    }

    public async Task<Unit> Handle(ChangePacketTypeCommand request, CancellationToken ct)
    {
        var packet = await _packetRepo.GetByIdAsync(request.Id);
        if (packet is null)
        {
            throw new Exception("Packet not found");
        }

        var type = await _packetTypeRepo.GetByIdAsync(request.PacketTypeId);
        if (type is null)
        {
            throw new Exception("Packet type not found");
        }

        packet.ChangeType(type.PacketTypeId);

        await _packetRepo.UpdateAsync(packet);

        return Unit.Value;
    }
}