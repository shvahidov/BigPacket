using Application.Interfaces;
using MediatR;

namespace Application.Features.Packets.Commands;

public class ChangePacketEndDateHandler : IRequestHandler<ChangePacketEndDateCommand>
{
    private readonly IPacketRepository _repo;

    public ChangePacketEndDateHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(ChangePacketEndDateCommand request, CancellationToken ct)
    {
        var packet = await _repo.GetByIdAsync(request.Id);

        if (packet is null)
        {
            throw new Exception("Packet not found");
        }

        // Домен сам решает — можно или нет
        packet.ChangeEndDate(request.NewEndDate);

        await _repo.UpdateAsync(packet);

        return Unit.Value;
    }
}