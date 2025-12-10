using Domain.Entities;
using MediatR;

namespace Application.Features.Packets.Commands;

public record CreatePacketCommand(
    Guid PacketTypeId,
    Guid PacketStatusId,
    string? PacketKey,
    DateTime EndDate) : IRequest<Packet>;