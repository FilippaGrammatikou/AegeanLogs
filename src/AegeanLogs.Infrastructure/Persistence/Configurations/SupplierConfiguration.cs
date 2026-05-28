using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;
public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(supplier => supplier.Id);

        builder.Property(supplier => supplier.Name).IsRequired().HasMaxLength(200);
        builder.Property(supplier => supplier.ServiceCategory).IsRequired().HasConversion<string>().HasMaxLength(100);
        builder.Property(supplier => supplier.ContactEmail).IsRequired().HasMaxLength(256);
        builder.Property(supplier => supplier.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.HasIndex(supplier => supplier.Name).IsUnique();
        builder.HasIndex(supplier => supplier.ServiceCategory);
        builder.HasIndex(supplier => supplier.IsActive);
    }
}
