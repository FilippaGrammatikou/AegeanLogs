using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;
public class ServiceJobConfiguration : IEntityTypeConfiguration<ServiceJob>
{
    public void Configure(EntityTypeBuilder<ServiceJob> builder)
    {
        builder.ToTable("ServiceJobs");

        builder.HasKey(serviceJob => serviceJob.Id);

        builder.Property(serviceJob => serviceJob.Title).IsRequired().HasMaxLength(200);
        builder.Property(serviceJob => serviceJob.Description).HasMaxLength(1000);
        builder.Property(serviceJob => serviceJob.RequirementLevel).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(serviceJob => serviceJob.ReadinessImpact).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(serviceJob => serviceJob.Status).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(serviceJob => serviceJob.SupplierNotes).HasMaxLength(2000);
        builder.Property(serviceJob => serviceJob.EvidenceFileName).HasMaxLength(300);

        builder.HasIndex(serviceJob => serviceJob.PortCallId);
        builder.HasIndex(serviceJob => serviceJob.SupplierId);
        builder.HasIndex(serviceJob => serviceJob.Status);
        builder.HasIndex(serviceJob => serviceJob.Deadline);
        builder.HasIndex(serviceJob => serviceJob.ReadinessImpact);
        builder.HasIndex(serviceJob => new {serviceJob.SupplierId, serviceJob.Status,serviceJob.Deadline});
        builder.HasIndex(serviceJob => new { serviceJob.PortCallId, serviceJob.Status});
        
        builder.HasOne(serviceJob => serviceJob.PortCall).WithMany(portCall=> portCall.ServiceJobs)
                                                                                             .HasForeignKey(serviceJob=> serviceJob.PortCallId)
                                                                                             .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(serviceJob=> serviceJob.ServiceType).WithMany(serviceType=>  serviceType.ServiceJobs)
                                                                                                .HasForeignKey(serviceJob=> serviceJob.ServiceTypeId)
                                                                                                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(serviceJob => serviceJob.Supplier).WithMany(supplier => supplier.ServiceJobs)
                                                                                                .HasForeignKey(serviceJob => serviceJob.SupplierId)
                                                                                                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(serviceJob => serviceJob.CheckedByUser).WithMany().HasForeignKey(serviceJob => serviceJob.CheckedByUserId)
                                                                                                       .OnDelete(DeleteBehavior.Restrict);
    }
}
