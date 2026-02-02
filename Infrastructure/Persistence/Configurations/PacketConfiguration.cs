using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PacketConfiguration : IEntityTypeConfiguration<Packet>
{
    public void Configure(EntityTypeBuilder<Packet> builder)
    {
        builder.HasKey(x => x.PacketId);

        builder.Property(x => x.PacketKey)
            .IsRequired()
            .HasMaxLength(100);

        // Храним SmartEnum как int
        builder.Property(x => x.Status)
            .HasConversion(
                status => status.Value,           // в БД
                value => PacketStatusEnum.FromValue(value)) // из БД
            .IsRequired();

        builder.Property(x => x.AddDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
    }
}
