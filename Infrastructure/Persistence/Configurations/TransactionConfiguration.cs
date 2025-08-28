using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount).IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        // enum → string
        builder.Property(t => t.Status)
            .HasConversion(
                v => v.ToString(),
                v => (TransactionStatus)Enum.Parse(typeof(TransactionStatus), v))
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(t => t.FromAccount)
            .WithMany()
            .HasForeignKey(t => t.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ToAccount)
            .WithMany()
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
