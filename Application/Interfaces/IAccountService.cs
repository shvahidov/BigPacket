namespace Application.Interfaces;

public interface IAccountService
{
    Task TransferAsync(int fromAccountId, int toAccountId, decimal amount);
}