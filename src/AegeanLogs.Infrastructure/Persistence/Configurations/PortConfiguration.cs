using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;

public class PortConfiguration : IEntityTypeConfiguration<Port>
{
    public void Configure(EntityTypeBuilder<Port> builder)
    {
        builder.ToTable("Ports");

        builder.HasKey(port => port.Id);

        builder.Property(port => port.Name).IsRequired().HasMaxLength(200);
        builder.Property(port => port.Country).IsRequired().HasMaxLength(100);
        builder.Property(port => port.Country).IsRequired().HasMaxLength(20);

        builder.HasIndex(port => port.UnLocode).IsUnique();

        builder.HasIndex(port => port.IsActive);
    }
}
