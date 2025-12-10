using Application.Interfaces;
using MediatR;

namespace Application.Features.Packets.Commands;

public class DeletePacketHandler : IRequestHandler<DeletePacketCommand, Unit>
{
    private readonly IPacketRepository _repo;

    public DeletePacketHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(DeletePacketCommand request, CancellationToken ct)
    {
        await _repo.DeleteAsync(request.Id);
        return Unit.Value;
    }
}