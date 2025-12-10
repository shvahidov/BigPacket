using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Queries;

public record GetAllPacketsQuery() : IRequest<IEnumerable<Packet>>;
