using MediatR;

namespace Application.Features.Packets.Commands;

public record ActivatePacketCommand(Guid PacketId) : IRequest<Unit>;
