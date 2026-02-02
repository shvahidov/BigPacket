using MediatR;

namespace Application.Features.Packets.Commands;

public class ChangePacketEndDateCommand : IRequest
{
    public Guid Id { get; set; } // Id пакета

    public DateTime NewEndDate { get; set; }
}