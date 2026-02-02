using MediatR;

namespace Application.Features.Packets.Commands;

public class ChangePacketTypeCommand : IRequest
{
    public Guid Id { get; set; } // Id пакета

    public Guid PacketTypeId { get; set; } // новый тип
}