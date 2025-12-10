using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Queries;

public class GetAllPacketsHandler : IRequestHandler<GetAllPacketsQuery, IEnumerable<Packet>>
{
    private readonly IPacketRepository _repo;

    public GetAllPacketsHandler(IPacketRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Packet>> Handle(GetAllPacketsQuery request, CancellationToken ct)
        => _repo.GetAllAsync();
}