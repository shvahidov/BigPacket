using System.Transactions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Transaction = Domain.Entities.Transaction;
using TransactionStatus = Domain.Entities.TransactionStatus;

namespace Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Month> Months { get; set; }

    public DbSet<Packet> Packets { get; set; }

    public DbSet<PacketStatus> PacketStatus { get; set; }

    public DbSet<PacketType> PacketTypes { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<TransactionStatus> TransactionStatus { get; set; }

    public DbSet<User> Users { get; set; }
}
