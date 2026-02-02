namespace Domain.Entities;

using Domain.Enums;

public class Packet
{
    public Guid PacketId { get; private set; }

    public Guid PacketTypeId { get; private set; }

    public string PacketKey { get; private set; } = null!;

    public DateTime AddDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public PacketStatusEnum Status { get; private set; }

    protected Packet()
    {
    }

    public static Packet Create(
        PacketType packetType,
        string? packetKey,
        DateTime endDate)
    {
        if (endDate <= DateTime.UtcNow)
        {
            throw new Exception("End date must be in the future");
        }

        return new Packet
        {
            PacketId = Guid.NewGuid(),
            PacketTypeId = packetType.PacketTypeId,
            PacketKey = string.IsNullOrWhiteSpace(packetKey)
                ? Guid.NewGuid().ToString("N")
                : packetKey,
            AddDate = DateTime.UtcNow,
            EndDate = endDate,
            Status = PacketStatusEnum.Pending
        };
    }

    public void Activate()
    {
        if (Status != PacketStatusEnum.Pending)
        {
            throw new InvalidOperationException("Packet can be activated only from Pending state");
        }

        Status = PacketStatusEnum.Active;
    }

    public void CheckExpiration(DateTime now)
    {
        if (Status == PacketStatusEnum.Active && EndDate <= now)
        {
            Status = PacketStatusEnum.Expired;
        }
    }

    public void Disable()
    {
        if (Status == PacketStatusEnum.Expired)
        {
            throw new InvalidOperationException("Expired packet cannot be disabled");
        }

        if (Status == PacketStatusEnum.Disabled)
        {
            throw new InvalidOperationException("Packet is already disabled");
        }

        Status = PacketStatusEnum.Disabled;
    }

    public void ChangeEndDate(DateTime newEndDate)
    {
        if (Status == PacketStatusEnum.Expired || Status == PacketStatusEnum.Disabled)
        {
            throw new InvalidOperationException("Cannot change end date for expired or disabled packet");
        }

        if (newEndDate <= DateTime.UtcNow)
        {
            throw new Exception("End date must be in the future");
        }

        EndDate = newEndDate;
    }

    public void ChangeType(Guid newPacketTypeId)
    {
        if (Status != PacketStatusEnum.Pending)
        {
            throw new InvalidOperationException("Packet type can be changed only in Pending state");
        }

        PacketTypeId = newPacketTypeId;
    }
}