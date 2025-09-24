namespace Application.DTOs;

public class CreatePacketDto
{
    public Guid PacketTypeId { get; set; }

    public Guid PacketStatusId { get; set; }

    public string PacketKey { get; set; } = string.Empty;

    public DateTime EndDate { get; set; }
}