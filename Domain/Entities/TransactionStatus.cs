namespace Domain.Entities;

public class TransactionStatus
{
    public Guid TransactionStatusId { get; set; }

    public required string TransactionStatusName { get; set; }
}