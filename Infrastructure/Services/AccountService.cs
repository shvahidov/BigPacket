using Application.Interfaces;
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
                throw new Exception("One or both accounts not found");

            if (from.Balance < amount)
                throw new Exception("Insufficient funds");

            from.Balance -= amount;
            to.Balance += amount;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}