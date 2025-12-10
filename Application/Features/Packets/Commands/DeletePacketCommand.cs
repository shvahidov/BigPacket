using MediatR;

namespace Application.Features.Packets.Commands;

public record DeletePacketCommand(Guid Id) : IRequest<Unit>;