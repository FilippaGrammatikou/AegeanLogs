using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
{
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.ToTable("ServiceTypes");

        builder.HasKey(serviceType => serviceType.Id);

        builder.Property(serviceType => serviceType.Code).IsRequired().HasMaxLength(60);
        builder.Property(serviceType => serviceType.Name).IsRequired().HasMaxLength(150);
        builder.Property(serviceType => serviceType.Category).IsRequired().HasConversion<string>().HasMaxLength(100);
        builder.Property(serviceType => serviceType.Description).HasMaxLength(1000);

        builder.HasIndex(serviceType => serviceType.Code).IsUnique();
        builder.HasIndex(serviceType=> serviceType.Category);
        builder.HasIndex(serviceType=> serviceType.IsActive);
    }
}
