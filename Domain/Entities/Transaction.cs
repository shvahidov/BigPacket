namespace Domain.Entities;

public class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid PacketId { get; set; }

    public Guid PacketTypeId { get; set; }

    public Guid UserId { get; set; }

    public Guid TransactionStatusId { get; set; }

    public required string Response { get; set; }

    public DateTime TradedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public required string PhoneNumber { get; set; }

    public required string ParamsJson { get; set; }

    public required TransactionStatus TransactionStatus { get; set; }

    public required Packet Packet { get; set; }

    public required PacketType PacketType { get; set; }

    public required User User { get; set; }
}