using Application.Interfaces;
using MediatR;

namespace Application.Features.Packets.Commands;

public class DisablePacketHandler : IRequestHandler<DisablePacketCommand, Unit>
{
    private readonly IPacketRepository _repo;

    public DisablePacketHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(DisablePacketCommand request, CancellationToken ct)
    {
        var packet = await _repo.GetByIdAsync(request.PacketId);

        if (packet is null)
        {
            throw new Exception("Packet not found");
        }

        packet.Disable(); // доменное правило

        await _repo.UpdateAsync(packet);

        return Unit.Value;
    }
}