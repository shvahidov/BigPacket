using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Queries;

public record GetPacketByIdQuery(Guid Id) : IRequest<Packet?>;