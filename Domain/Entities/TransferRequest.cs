namespace Domain.Entities;

public class TransferRequest
{
    public TransferRequest(int fromAccountId, int toAccountId, decimal amount)
    {
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }

    public int FromAccountId { get; set; }

    public int ToAccountId { get; set; }

    public decimal Amount { get; set; }
}