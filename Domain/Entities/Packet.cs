namespace Domain.Entities;

public class Packet
{
    public Guid PacketId { get; set; }

    public Guid PacketTypeId { get; set; }

    public required string PacketKey { get; set; }

    public DateTime AddDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid PacketStatusId { get; set; }

    public PacketType? PacketType { get; set; }

    public PacketStatus? PacketStatus { get; set; }
}