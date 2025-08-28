namespace Domain.Entities;

using Domain.Enums;

public class Transaction
{
    public int Id { get; set; }

    public int FromAccountId { get; set; }

    public int ToAccountId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public TransactionStatus Status { get; set; }

    // Навигация
    public Account? FromAccount { get; set; }

    public Account? ToAccount { get; set; }
}