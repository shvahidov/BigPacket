namespace Domain.Entities;

public class PacketStatus
{
    public Guid PacketStatusId { get; set; }

    public required string PacketStatusName { get; set; }
}