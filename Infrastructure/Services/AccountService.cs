/*using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task TransferAsync(int fromAccountId, int toAccountId, decimal amount)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var from = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == fromAccountId);
            var to = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == toAccountId);

            if (from == null || to == null)
            {
                throw new Exception("One or both accounts not found");
            }

            if (fromAccountId == toAccountId)
            {
                throw new Exception("Cannot transfer to the same account");
            }

            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }

            if (from.Balance < amount)
            {
                throw new Exception("Insufficient funds");
            }

            from.Balance -= amount;
            to.Balance += amount;

            // Сохраняем транзакцию
            var newTransaction = new Transaction
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = amount,
                Status = TransactionStatus.Success,
                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(newTransaction);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            var failedTransaction = new Transaction
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = amount,
                Status = TransactionStatus.Failed,
                CreatedAt = DateTime.UtcNow
            };

            // Добавляем вне предыдущей транзакции
            _context.Transactions.Add(failedTransaction);
            await _context.SaveChangesAsync();

            throw;
        }
    }
}*/