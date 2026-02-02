using MediatR;

namespace Application.Features.Packets.Commands;

public record DisablePacketCommand(Guid PacketId) : IRequest<Unit>;
