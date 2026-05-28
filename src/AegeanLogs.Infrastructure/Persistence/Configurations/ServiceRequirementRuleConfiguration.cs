using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;

public class ServiceRequirementRuleConfiguration : IEntityTypeConfiguration<ServiceRequirementRule>
{
    public void Configure(EntityTypeBuilder<ServiceRequirementRule> builder)
    {
        builder.ToTable("ServiceRequirementRules");

        builder.HasKey(rule => rule.Id);

        builder.Property(rule=> rule.PortCallPurpose).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(rule => rule.RequirementLevel).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(rule => rule.ReadinessImpact).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(rule => rule.DeadlineAnchor).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(rule => rule.Rationale).HasMaxLength(1000);

        builder.HasIndex(rule => new { rule.PortCallPurpose, rule.ServiceTypeId }).IsUnique();
        builder.HasIndex(rule => rule.IsActive);

        builder.HasOne(rule=> rule.ServiceType).WithMany(serviceType => serviceType.RequirementRules)
                                                                          .HasForeignKey(rule => rule.ServiceTypeId)
                                                                          .OnDelete(DeleteBehavior.Restrict);
    }
}
