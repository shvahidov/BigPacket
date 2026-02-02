using Application.Interfaces;
using MediatR;

namespace Application.Features.Packets.Commands;

public class ActivatePacketHandler : IRequestHandler<ActivatePacketCommand, Unit>
{
    private readonly IPacketRepository _repo;

    public ActivatePacketHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(ActivatePacketCommand request, CancellationToken ct)
    {
        var packet = await _repo.GetByIdAsync(request.PacketId);

        if (packet == null)
        {
            throw new Exception("Packet not found");
        }

        packet.Activate();

        await _repo.UpdateAsync(packet);

        return Unit.Value;
    }
}