using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Commands;

public class CreatePacketHandler : IRequestHandler<CreatePacketCommand, Packet>
{
    private readonly IPacketRepository _repo;
    private readonly IPacketTypeRepository _repotype;

    public CreatePacketHandler(IPacketRepository repo, IPacketTypeRepository repotype)
    {
        _repo = repo;
        _repotype = repotype;
    }

    public async Task<Packet> Handle(CreatePacketCommand request, CancellationToken ct)
    {
        var type = await _repotype.GetByIdAsync(request.PacketTypeId);
        if (type == null)
        {
            throw new Exception("Packet type not found");
        }

        var packet = Packet.Create(type, request.PacketKey, request.EndDate);

        await _repo.AddAsync(packet);
        return packet;
    }
}