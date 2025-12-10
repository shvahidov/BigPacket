using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Queries;

public class GetPacketByIdHandler : IRequestHandler<GetPacketByIdQuery, Packet?>
{
    private readonly IPacketRepository _repo;

    public GetPacketByIdHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public Task<Packet?> Handle(GetPacketByIdQuery request, CancellationToken ct)
        => _repo.GetByIdAsync(request.Id);
}