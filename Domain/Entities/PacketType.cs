namespace Domain.Entities;

public class PacketType
{
    public Guid PacketTypeId { get; set; }

    public required string PacketTypeName { get; set; }

    public short Sort { get; set; }

    public bool IsForSoghd { get; set; }
}